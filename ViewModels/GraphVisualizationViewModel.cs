using AdvancedCalculator.Structures;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GraphShape.Algorithms.OverlapRemoval;
using QuikGraph.Algorithms;
using QuikGraph.Algorithms.Search;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;

namespace AdvancedCalculator.ViewModels
{
    public partial class GraphVisualizationViewModel : ObservableObject
    {
        [ObservableProperty]
        private PocGraph _graph;

        [ObservableProperty]
        private ObservableCollection<string> _algorithms;

        [ObservableProperty]
        private OverlapRemovalParameters _overlapRemovalParameters;

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

        private PocGraph _backupGraph;

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
            OverlapRemovalParameters = new OverlapRemovalParameters { HorizontalGap = 25, VerticalGap = 25 };
            var clone = Graph.Clone();
            _backupGraph = new PocGraph();
            _backupGraph.AddVertexRange(clone.Vertices);
            _backupGraph.AddEdgeRange(clone.Edges);
        }

        [RelayCommand]
        private void RunBreadthFirstSearch()
        {
            BackUpGraph();

            List<PocEdge> bfsResult = [];
            var bfs = new BreadthFirstSearchAlgorithm<PocVertex, PocEdge>(Graph);
            bfs.TreeEdge += e => bfsResult.Add(e);

            if (!string.IsNullOrWhiteSpace(VertexId))
            {
                bfs.Compute(GetVertexById(VertexId)!);
            }
            else
            {
                MessageBox.Show("Please enter a vertex ID below!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            foreach (var edge in bfsResult)
            {
                edge.Color = Brushes.Red;
            }

            ClearEdges();
            Graph.AddEdgeRange(bfsResult);
            OnPropertyChanged(nameof(Graph));

            MessageBox.Show($"Edge path:\n{string.Join(", ", bfsResult)}", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        [RelayCommand]
        private void RunDijkstraShortestPath()
        {
            if (!string.IsNullOrWhiteSpace(SourceVertexId) && !string.IsNullOrWhiteSpace(TargetVertexId))
            {
                Func<PocEdge, double> edgeCostFunc = e => e.Weight;
                var source = GetVertexById(SourceVertexId);
                var target = GetVertexById(TargetVertexId);
                var dijsktra = Graph!.ShortestPathsDijkstra(edgeCostFunc, source);

                if (dijsktra(target, out IEnumerable<PocEdge> path))
                {
                    foreach (var edge in path)
                    {
                        edge.Color = Brushes.Red;
                    }

                    ClearEdges();
                    Graph.AddEdgeRange(path);
                    OnPropertyChanged(nameof(Graph));

                    MessageBox.Show($"Edge path:\n{string.Join(", ", path)}", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Please enter the source and target vertex ID below!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        [RelayCommand]
        private void RestoreGraph()
        {
            var clone = _backupGraph.Clone();
            Graph = new PocGraph();
            Graph.AddVertexRange(clone.Vertices);
            // TODO: fix copying issues
            foreach (var edge in clone.Edges)
            {
                edge.Color = Brushes.Silver;
            }
            Graph.AddEdgeRange(clone.Edges);
            OnPropertyChanged(nameof(Graph));
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

        private PocVertex? GetVertexById(string id)
        {
            return Graph.Vertices.FirstOrDefault(v => v.ID == id);
        }

        //private PocEdge? GetEdgeById(string id)
        //{
        //    return Graph.Edges.FirstOrDefault(e => e.ID == id);
        //}

        private void ClearEdges()
        {
            foreach (var vertex in Graph.Vertices)
            {
                Graph.ClearEdges(vertex);
            }
        }

        private void BackUpGraph()
        {
            var clone = Graph.Clone();
            _backupGraph = new PocGraph();
            _backupGraph.AddVertexRange(clone.Vertices);
            _backupGraph.AddEdgeRange(clone.Edges);
        }

        private static PocEdge EdgeFactory(PocVertex source, PocVertex target, int weight = 0)
        {
            return new PocEdge($"{source.ID}->{target.ID}", source, target, weight);
        }
    }
}