using ApiRestVideoGames.Models;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.IO;


namespace ApiRestVideoGames.Services
{
    public class ImportService
    {
        public List<VideoGame> ImportCsv(string path)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                MissingFieldFound = null,          // Ignora campos faltantes
                HeaderValidated = null,            // Ignora headers distintos
                BadDataFound = null                // Ignora datos corruptos
            };

            using var reader = new StreamReader(path);
            using var csv = new CsvReader(reader, config);

            // Convertir valores "N/A" a null en Year
            csv.Context.TypeConverterOptionsCache.GetOptions<int?>()
                .NullValues.Add("N/A");

            return csv.GetRecords<VideoGame>().ToList();
        }

    }
}
