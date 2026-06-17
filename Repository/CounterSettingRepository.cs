using CounterInfo.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CounterInfo.Repository
{
    public class CounterSettingRepository
    {
        SQLiteAsyncConnection? database;

        async Task Init()
        {
            if (database is not null) return;
            database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            var result = await database.CreateTableAsync<CounterSetting>();
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

        public async Task ChangeOrderAsync(int id)
        {
            await Init();
            await database!.RunInTransactionAsync(tran => {
                // database calls inside the transaction
                var t1 = tran.Table<CounterSetting>().Where(i => i.Order == id).FirstOrDefault();
                t1.Order++;
                tran.Update(t1);
            });
        }
    }
}
