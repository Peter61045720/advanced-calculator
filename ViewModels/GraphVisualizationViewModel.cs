using AdvancedCalculator.Structures;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows;

namespace AdvancedCalculator.ViewModels
{
    public partial class GraphVisualizationViewModel : ObservableObject
    {
        [ObservableProperty]
        private PocGraph _graph;

        [ObservableProperty]
        private ObservableCollection<string> _algorithms;

        [ObservableProperty]
        private string _selectedAlgorithm;

        [ObservableProperty]
        private string? _vertexId;

        [ObservableProperty]
        private string? _sourceVertexId;

        [ObservableProperty]
        private string? _targetVertexId;

        [ObservableProperty]
        private double _zoomFactor = 1.5;

        [ObservableProperty]
        private double _minZoom = 0.5;

        [ObservableProperty]
        private double _maxZoom = 2.0;

        public GraphVisualizationViewModel()
        {
            Graph = new PocGraph();

            List<PocVertex> vertices =
            [
                new PocVertex("A"),
                new PocVertex("B"),
            ];

            Graph.AddVertexRange(vertices);
            Graph.AddEdge(EdgeFactory(vertices[0], vertices[1]));

            Algorithms =
            [
                "BoundedFR",
                "Circular",
                "CompoundFDP",
                "EfficientSugiyama",
                "FR",
                "ISOM",
                "KK",
                "LinLog",
                "Tree",
            ];

            SelectedAlgorithm = "BoundedFR";
        }

        [RelayCommand]
        private void AddVertex()
        {
            if (!string.IsNullOrWhiteSpace(VertexId))
            {
                var vertexToAdd = Graph.Vertices.FirstOrDefault(v => v.ID == VertexId);

                if (vertexToAdd == null)
                {
                    Graph.AddVertex(new PocVertex(VertexId));
                    OnPropertyChanged(nameof(Graph));
                    VertexId = string.Empty;
                }
                else
                {
                    MessageBox.Show("This vertex already exists.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Input cannot be empty. Please enter a valid value.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        [RelayCommand]
        private void RemoveVertex()
        {
            var vertexToRemove = Graph.Vertices.FirstOrDefault(v => v.ID == VertexId);

            if (!string.IsNullOrWhiteSpace(VertexId) && vertexToRemove != null)
            {
                Graph.RemoveVertex(vertexToRemove);
                OnPropertyChanged(nameof(Graph));
                VertexId = string.Empty;
            }
            else
            {
                MessageBox.Show("The vertex cannot be found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        [RelayCommand]
        private void AddEdge()
        {
            if (!string.IsNullOrWhiteSpace(SourceVertexId) && !string.IsNullOrWhiteSpace(TargetVertexId))
            {
                var edgeToAdd = Graph.Edges.FirstOrDefault(e => e.Source.ID == SourceVertexId && e.Target.ID == TargetVertexId);

                if (edgeToAdd == null)
                {
                    var sourceVertex = Graph.Vertices.FirstOrDefault(v => v.ID == SourceVertexId);
                    var targetVertex = Graph.Vertices.FirstOrDefault(v => v.ID == TargetVertexId);

                    if (sourceVertex != null & targetVertex != null)
                    {
                        Graph.AddEdge(EdgeFactory(sourceVertex!, targetVertex!));
                        OnPropertyChanged(nameof(Graph));
                        SourceVertexId = string.Empty;
                        TargetVertexId = string.Empty;
                    }
                    else
                    {
                        MessageBox.Show("The source or target vertex cannot be found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("This edge already exists.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Input cannot be empty. Please enter a valid value.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        [RelayCommand]
        private void RemoveEdge()
        {
            if (!string.IsNullOrWhiteSpace(SourceVertexId) && !string.IsNullOrWhiteSpace(TargetVertexId))
            {
                var edgeToRemove = Graph.Edges.FirstOrDefault(e => e.Source.ID == SourceVertexId && e.Target.ID == TargetVertexId);

                if (edgeToRemove != null)
                {
                    Graph.RemoveEdge(edgeToRemove);
                    OnPropertyChanged(nameof(Graph));
                    SourceVertexId = string.Empty;
                    TargetVertexId = string.Empty;
                }
                else
                {
                    MessageBox.Show("The edge cannot be found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Input cannot be empty. Please enter a valid value.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private static PocEdge EdgeFactory(PocVertex source, PocVertex target)
        {
            return new PocEdge($"{source.ID}->{target.ID}", source, target);
        }
    }
}