using _Project.Scripts.Data;

namespace _Project.Scripts.Services.SaveLoad
{
    public interface ISaveLoadService
    {
        PersistentData Load();
        void Save(PersistentData data);
    }
}