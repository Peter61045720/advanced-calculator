using AdvancedCalculator.Structures;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace AdvancedCalculator.ViewModels
{
    // TODO: delete backup code
    public partial class GraphVisualizationViewModel : ObservableObject
    {
        //[ObservableProperty]
        //private BidirectionalGraph<object, IEdge<object>> _graph;

        [ObservableProperty]
        private PocGraph _graph;

        [ObservableProperty]
        private ObservableCollection<string> _algorithms;

        [ObservableProperty]
        private string _selectedAlgorithm;

        public GraphVisualizationViewModel()
        {
            //Graph = new BidirectionalGraph<object, IEdge<object>>();

            //Graph.AddVertex("A");
            //Graph.AddVertex("B");
            //Graph.AddVertex("C");
            //Graph.AddVertex("D");
            //Graph.AddVertex("E");
            //Graph.AddVertex("F");

            //Graph.AddEdge(new Edge<object>("A", "B"));
            //Graph.AddEdge(new Edge<object>("A", "D"));
            //Graph.AddEdge(new Edge<object>("A", "F"));
            //Graph.AddEdge(new Edge<object>("B", "A"));
            //Graph.AddEdge(new Edge<object>("C", "F"));
            //Graph.AddEdge(new Edge<object>("C", "F"));
            //Graph.AddEdge(new Edge<object>("D", "E"));
            //Graph.AddEdge(new Edge<object>("E", "B"));

            Graph = new PocGraph();

            List<PocVertex> vertices =
            [
                new PocVertex("A"),
                new PocVertex("B"),
                new PocVertex("C"),
                new PocVertex("D"),
                new PocVertex("E"),
                new PocVertex("F"),
            ];

            Graph.AddVertexRange(vertices);

            Graph.AddEdgeRange(new[]
            {
                EdgeFactory(vertices[0], vertices[1]),
                EdgeFactory(vertices[0], vertices[3]),
                EdgeFactory(vertices[0], vertices[5]),
                EdgeFactory(vertices[1], vertices[0]),
                EdgeFactory(vertices[2], vertices[5]),
                EdgeFactory(vertices[2], vertices[5]),
                EdgeFactory(vertices[3], vertices[4]),
                EdgeFactory(vertices[4], vertices[1]),
            });

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

        private static PocEdge EdgeFactory(PocVertex source, PocVertex target)
        {
            return new PocEdge($"{source.ID}to{target.ID}", source, target);
        }
    }
}