using _Modules.SaveModule.Scripts.Interfaces;

namespace _Modules.SaveModule.Scripts.Data
{
    public class GameData:ISaveableEntity
    {
        public int Money; 
        public byte Level;
        public bool Haptic;
        public bool SFX;
        public string GetKey()
        {
            throw new System.NotImplementedException();
        }
    }
}