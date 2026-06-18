using CounterInfo.Models;
using SQLite;

namespace CounterInfo.Repository
{
    public class CounterSettingRepository
    {
        SQLiteAsyncConnection? database;

        async Task Init()
        {
            if (database is not null) return;
            database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            _ = await database.CreateTableAsync<CounterSetting>();
        }

        public async Task<List<CounterSetting>> GetItemsAsync()
        {
            await Init();
            return await database!.Table<CounterSetting>().ToListAsync();
        }

        public async Task<CounterSetting> GetItemAsync(int id)
        {
            await Init();
            return await database!.Table<CounterSetting>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveItemAsync(CounterSetting item)
        {
            await Init();
            return item.ID != 0 ? await database!.UpdateAsync(item) : await database!.InsertAsync(item);
        }

        public async Task<int> DeleteItemAsync(CounterSetting item)
        {
            await Init();
            return await database!.DeleteAsync(item);
        }

        public async Task<int> GetCountAsync()
        {
            await Init();
            return await database!.Table<CounterSetting>().CountAsync();
        }

        public async Task ChangeOrderAsync(int id, bool down = false)
        {
            await Init();
            await database!.RunInTransactionAsync(tran => {
                if (tran.Table<CounterSetting>().Where(w => w.ID == id).FirstOrDefault() is CounterSetting from)
                {
                    from.Order += down ? 1 : -1; 
                    if (tran.Table<CounterSetting>().Where(w => w.Order == from.Order).FirstOrDefault() is CounterSetting to)
                    {
                        to.Order -= down ? 1 : -1;
                        tran.Update(from);
                        tran.Update(to);
                    }                  
                }
            });
        }
    }
}
