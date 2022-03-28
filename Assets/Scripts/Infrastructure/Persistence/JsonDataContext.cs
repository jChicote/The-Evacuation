using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace TheEvacuation.Infrastructure.Persistence
{

    public class JsonDataContext : DataContext
    {

        #region - - - - - - Fields - - - - - -

        public string fileName;
        public string filePath = Application.persistentDataPath + "GameSaveData";

        #endregion Fields

        #region - - - - - - Methods - - - - - -

        public override async Task Load()
        {
            if (!File.Exists(filePath)) return;
            using var reader = new StreamReader(filePath);
            var jsonString = await reader.ReadToEndAsync();
            JsonUtility.FromJsonOverwrite(jsonString, data);
        }

        public override async Task Save()
        {
            var jsonString = JsonUtility.ToJson(data);
            using var writer = new StreamWriter(filePath);
            await writer.WriteAsync(jsonString);
        }

        #endregion Methods

    }

}
