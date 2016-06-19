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

    }
}
