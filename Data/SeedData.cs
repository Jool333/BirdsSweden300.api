using System.Text.Json;
using BirdsSweden300.api.Entities;

namespace BirdsSweden300.api.Data
{
    public static class SeedData
    {
        public static async Task LoadBirdData(BirdsContext context)
        {
            var options = new JsonSerializerOptions{
                PropertyNameCaseInsensitive = true
            };

            if(context.Birds.Any()) return;
            var json = System.IO.File.ReadAllText("Data/json/birds.json");
            
            var birds = JsonSerializer.Deserialize<List<Bird>>(json,options);

            if(birds is not null && birds.Count >0){
                await context.Birds.AddRangeAsync(birds);
                await context.SaveChangesAsync();
            }
        }
        
    }
}