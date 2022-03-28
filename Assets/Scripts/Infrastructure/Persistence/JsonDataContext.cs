using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace TheEvacuation.Infrastructure.Persistence
{

    public class JsonDataContext : DataContext
    {

        #region - - - - - - Fields - - - - - -

        public string fileName = "GameSaveData";

        #endregion Fields

        #region - - - - - - Methods - - - - - -

        public override async Task Load()
        {
            if (!File.Exists(FilePath)) return;
            using var reader = new StreamReader(FilePath);
            var jsonString = await reader.ReadToEndAsync();
            JsonUtility.FromJsonOverwrite(jsonString, data);
        }

        public override async Task Save()
        {
            var jsonString = JsonUtility.ToJson(data);
            using var writer = new StreamWriter(FilePath);
            await writer.WriteAsync(jsonString);
        }

        private string FilePath => $"{ Application.persistentDataPath}/{fileName}.json";

        #endregion Methods

    }

}
