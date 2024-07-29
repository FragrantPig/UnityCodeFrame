//BUILT BY SCRIPT!!! DO NOT EDITOR!!!
namespace Scripts.Game.CSV
{
    public partial class MasterText : IDeepCopyable<MasterText>
    {
        
        public int category { get; set; }
        public int id { get; set; }
        public string text { get; set; }
        
        public MasterText DeepCopy()
        {
            MasterText result = new MasterText();
            result.category = category;
            result.id = id;
            result.text = text;
            return result;
        }
    }
}