namespace AdvancedCalculator.Structures
{
    public class PocVertex
    {
        public string ID { get; }

        public PocVertex(string id)
        {
            ID = id;
        }

        public override string ToString()
        {
            return ID;
        }
    }
}