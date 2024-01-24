using QuikGraph;

namespace AdvancedCalculator.Structures
{
    public class PocEdge : Edge<PocVertex>
    {
        public string ID { get; }

        public PocEdge(string id, PocVertex source, PocVertex target) : base(source, target)
        {
            ID = id;
        }
    }
}