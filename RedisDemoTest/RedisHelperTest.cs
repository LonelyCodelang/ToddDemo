using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace RedisDemoTest
{
    public class RedisHelperTest
    {
        redisDemo.RedisHelper helper = null;

        public RedisHelperTest()
        {
            helper = new redisDemo.RedisHelper();
        }

        [Fact]
        public void SetStringKeyTest()
        {
            bool value = helper.SetStringKey("key", "测试value");
            Assert.True(value);
        }

        [Fact]
        public void SetStringKeyTest1()
        {
            List<KeyValuePair<RedisKey, RedisValue>> list = new List<KeyValuePair<RedisKey, RedisValue>>();
            list.Add(new KeyValuePair<RedisKey, RedisValue>("key1", "测试值1"));
            list.Add(new KeyValuePair<RedisKey, RedisValue>("key2", "测试值2"));
            list.Add(new KeyValuePair<RedisKey, RedisValue>("key3", "测试值3"));
            bool value = helper.SetStringKey(list.ToArray());
            Assert.True(value);
        }

        [Fact]
        public void SetStringKeyObjTest()
        {
            Customer c = new Customer() { Name = "张三", Age = 10, Address = "广州市越秀区水荫路" };
            bool value = helper.SetStringKey<Customer>("c1", c);
            Assert.True(value);
        }

        [Fact]
        public void SetStringKeyListObjTest()
        {
            List<Customer> list = new List<Customer>();
            list.Add(new Customer() { Name = "路飞", Age = 15, Address = "xxxxxx" });
            list.Add(new Customer() { Name = "路飞1", Age = 16, Address = "xxxxxx111111" });
            list.Add(new Customer() { Name = "路飞1", Age = 18, Address = "xxxxxx222222" });
            bool value = helper.SetStringKey<List<Customer>>("cl1", list);
            Assert.True(value);
        }

        [Fact]
        public void GetValueByKeyTest()
        {
            string key = "redisx1";
            string value = "一个测试的值";
            helper.SetStringKey(key, value);
            string rvalue = helper.GetStringKey(key);
            Assert.Equal(value, rvalue);
        }

        [Fact]
        public void GetArrValueByTest1()
        {
            List<KeyValuePair<RedisKey, RedisValue>> list = new List<KeyValuePair<RedisKey, RedisValue>>();
            list.Add(new KeyValuePair<RedisKey, RedisValue>("keycl1", "测试值1"));
            list.Add(new KeyValuePair<RedisKey, RedisValue>("keycl2", "测试值2"));
            list.Add(new KeyValuePair<RedisKey, RedisValue>("keycl3", "测试值3"));
            helper.SetStringKey(list.ToArray());

            List<RedisKey> listKey = new List<RedisKey>();
            listKey.Add("keycl1");
            listKey.Add("keycl2");
            listKey.Add("keycl3");
            RedisValue[] arr = helper.GetStringKey(listKey);
            Assert.Equal(list.Count(), arr.Length);
        }

        [Fact]
        public void SetStringKeyListObjTest1()
        {
            List<Customer> list = new List<Customer>();
            list.Add(new Customer() { Name = "路飞", Age = 15, Address = "xxxxxx" });
            list.Add(new Customer() { Name = "路飞1", Age = 16, Address = "xxxxxx111111" });
            list.Add(new Customer() { Name = "路飞1", Age = 18, Address = "xxxxxx222222" });
            bool value = helper.SetStringKey<List<Customer>>("cl1", list);
            Assert.True(value);
        }

        [Fact]
        public void GetStringKeyListObjTest()
        {
            List<Customer> list = new List<Customer>();
            list.Add(new Customer() { Name = "路飞", Age = 15, Address = "xxxxxx" });
            list.Add(new Customer() { Name = "路飞1", Age = 16, Address = "xxxxxx111111" });
            list.Add(new Customer() { Name = "路飞1", Age = 18, Address = "xxxxxx222222" });
            bool value = helper.SetStringKey<List<Customer>>("cl1", list);

            List<Customer> listc = helper.GetStringKey<List<Customer>>("cl1");
            Assert.Equal(list.Count, listc.Count);
        }

        [Fact]
        public void HashSetTest()
        {
            List<Customer> list = new List<Customer>();
            list.Add(new Customer() { id = 1, Name = "路飞", Age = 15, Address = "xxxxxx" });
            list.Add(new Customer() { id = 2, Name = "路飞1", Age = 16, Address = "xxxxxx111111" });
            list.Add(new Customer() { id = 3, Name = "路飞1", Age = 18, Address = "xxxxxx222222" });

            helper.HashSet<Customer>("hakey1", list, new Func<Customer, string>(TestMothod));

        }

        [Fact]
        public void HashGetOneTest()
        {
            List<Customer> list = new List<Customer>();
            list.Add(new Customer() { id = 1, Name = "路飞", Age = 15, Address = "xxxxxx" });
            list.Add(new Customer() { id = 2, Name = "路飞1", Age = 16, Address = "xxxxxx111111" });
            list.Add(new Customer() { id = 3, Name = "路飞1", Age = 18, Address = "xxxxxx222222" });

            helper.HashSet<Customer>("hakey2", list, new Func<Customer, string>(TestMothod));

            Customer value = helper.GetHashKey<Customer>("hakey2", "2");
            Assert.Equal("路飞1", value.Name);
        }

        [Fact]
        public void HashGetOneTest2()
        {
            List<Customer> list = new List<Customer>();
            list.Add(new Customer() { id = 1, Name = "路飞", Age = 15, Address = "xxxxxx" });
            list.Add(new Customer() { id = 2, Name = "路飞1", Age = 16, Address = "xxxxxx111111" });
            list.Add(new Customer() { id = 3, Name = "路飞1", Age = 18, Address = "xxxxxx222222" });

            helper.HashSet<Customer>("hakey3", list, new Func<Customer, string>(TestMothod));

            List<RedisValue> listkey = new List<RedisValue>();
            listkey.Add(1);
            listkey.Add(2);

            List<Customer> listValue = helper.GetHashKey<Customer>("hakey3", listkey);
            Assert.Equal(listkey.Count, listValue.Count);
        }

        [Fact]
        public void GetHashAllTest()
        {
            List<Customer> list = new List<Customer>();
            list.Add(new Customer() { id = 1, Name = "路飞", Age = 15, Address = "xxxxxx" });
            list.Add(new Customer() { id = 2, Name = "路飞1", Age = 16, Address = "xxxxxx111111" });
            list.Add(new Customer() { id = 3, Name = "路飞1", Age = 18, Address = "xxxxxx222222" });

            helper.HashSet<Customer>("hakey4", list, new Func<Customer, string>(TestMothod));


            List<Customer> listValue = helper.HashGetAll<Customer>("hakey4");
            Assert.Equal(list.Count, listValue.Count);
        }


        [Fact]
        public void DeleteHaseTest()
        {
            List<Customer> list = new List<Customer>();
            list.Add(new Customer() { id = 1, Name = "路飞", Age = 15, Address = "xxxxxx" });
            list.Add(new Customer() { id = 2, Name = "路飞1", Age = 16, Address = "xxxxxx111111" });
            list.Add(new Customer() { id = 3, Name = "路飞1", Age = 18, Address = "xxxxxx222222" });

            helper.HashSet<Customer>("hakey5", list, new Func<Customer, string>(TestMothod));


            helper.DeleteHase("hakey5", "2");
            List<Customer> listValue = helper.HashGetAll<Customer>("hakey5");
            Assert.Equal(list.Count - 1, listValue.Count);
        }

        [Fact]
        public void KeyExistsTest()
        {
            helper.SetStringKey("xxxd1", "dd");
            bool value1 = helper.KeyExists("xxxd1");
            Assert.True(value1);
            bool value2 = helper.KeyExists("xxxd12");
            Assert.False(value2);
        }


        public string TestMothod(Customer item)
        {
            return item.id.ToString();
        }
    }

    public class Customer
    {
        public int id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
    }
}
