//BUILT BY SCRIPT!!! DO NOT EDITOR!!!
namespace Scripts.Game.CSV
{
    public partial class MasterLocalize : IDeepCopyable<MasterLocalize>
    {
        
        public string id { get; set; }
        public string cn { get; set; }
        public string en { get; set; }
        public string jp { get; set; }
        
        public MasterLocalize DeepCopy()
        {
            MasterLocalize result = new MasterLocalize();
            result.id = id;
            result.cn = cn;
            result.en = en;
            result.jp = jp;
            return result;
        }
    }
}