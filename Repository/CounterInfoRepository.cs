using CounterInfo.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CounterInfo.Repository
{
    public class CounterInfoRepository
    {
        SQLiteAsyncConnection? database;

        async Task Init()
        {
            if (database is not null) return;
            database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            var result = await database.CreateTableAsync<CounterInfomation>();
        }

        public async Task<List<CounterInfomation>> GetItemsAsync()
        {
            await Init();
            return await database!.Table<CounterInfomation>().ToListAsync();
        }

        public async Task<List<CounterInfomation>> GetCalledAsync()
        {
            await Init();
            return await database!.Table<CounterInfomation>().Where(t => t.Called).ToListAsync();
        }

        public async Task<CounterInfomation> GetItemAsync(int id)
        {
            await Init();
            return await database!.Table<CounterInfomation>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveItemAsync(CounterInfomation item)
        {
            await Init();
            return item.ID != 0 ? await database!.UpdateAsync(item) : await database!.InsertAsync(item);
        }

        public async Task<int> InsertItemAsync(CounterInfomation item)
        {
            await Init();
            return await database!.InsertAsync(item);
        }

        public async Task<int> DeleteItemAsync(CounterInfomation item)
        {
            await Init();
            return await database!.DeleteAsync(item);
        }

        public async Task ReceptAsync(int id, int absenceParcent)
        {
            await Init();
            if (await database!.Table<CounterInfomation>().Where(w => w.Counter == id).Where(t => t.ReceptTime == null).FirstOrDefaultAsync() is CounterInfomation ci)
            {
                var rnd = new System.Random();
                ci.ReceptTime = DateTime.Now;
                ci.Called = rnd.Next(100) < absenceParcent - 1;
                await database!.UpdateAsync(ci);
            }
        }
    }
}
