using QuikGraph;
using System.Windows.Media;

namespace AdvancedCalculator.Structures
{
    public class PocEdge : Edge<PocVertex>
    {
        public string ID { get; }

        public SolidColorBrush Color { get; set; }

        private int _weight;

        public int Weight
        {
            get { return _weight; }
            set { _weight = value >= 0 ? value : 0; }
        }

        public PocEdge(string id, PocVertex source, PocVertex target, int weight = 0) : base(source, target)
        {
            ID = id;
            Weight = weight;
            Color = Brushes.Silver;
        }
    }
}