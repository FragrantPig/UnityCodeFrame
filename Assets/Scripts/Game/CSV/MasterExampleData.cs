//BUILT BY SCRIPT!!! DO NOT EDITOR!!!
namespace Scripts.Game.CSV
{
    public partial class MasterExampleData : IDeepCopyable<MasterExampleData>
    {
        
        public int id { get; set; }
        public int label1 { get; set; }
        public int label2 { get; set; }
        
        public MasterExampleData DeepCopy()
        {
            MasterExampleData result = new MasterExampleData();
            result.id = id;
            result.label1 = label1;
            result.label2 = label2;
            return result;
        }
    }
}