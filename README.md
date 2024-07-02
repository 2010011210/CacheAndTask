# CacheAndTask
Cache and task demo 2024-06-30     

# 1.Cache   
 1.1 使用委托传入默认值   
  ```  
  var key = $"{nameof(CacheTest)}_Query_{nameof(DBHelper)}_{124}";
                Console.WriteLine($"Key:{key}");
  //默认值当做一个委托传进去。
  var result = CustomeCache.Find<List<Order>>(key, () => DBHelper.Query<Order>(key));     

  //  CustomeCache中的方法
  public static T Find<T>(string key, Func<T> fun) 
{
    var t = default(T);
    if (Exist(key))
    {
        t = Get<T>(key);
    }
    else 
    {
        t = fun.Invoke();
        customeCache.Add(key, t);
    }
    return t;
}    
```


 1.2 过期时间自动清除   
 ``` 
 /// <summary>
/// 添加缓存，需要添加过期时间
/// </summary>
/// <param name="key"></param>
/// <param name="value"></param>
/// <param name="seconds"></param>
public static void Add(string key, object value, int seconds)
{
    customeCache[key] = new KeyValuePair<object, DateTime>(value, DateTime.Now.AddSeconds(seconds));
}


public static T Get<T>(string key) 
{
    return (T)customeCache[key].Key;
}

/// <summary>
/// 检查的时候，也需要检查过期时间
/// </summary>
/// <param name="key"></param>
/// <returns></returns>
public static bool Exist(string key) 
{
    if (customeCache.ContainsKey(key))
    {
        var cacheValue = customeCache[key];
        if (cacheValue.Value > DateTime.Now)
        {
            return true;
        }
        else 
        {
            customeCache.Remove(key);  //过期了，直接删除
            return false;
        }

    }
    else 
    {
        return false;
    }
}
 ```  

 如果缓存长时间不用，需要定期清除，防止一致占用内存。可以在缓存的构造函数中启动一个任务  
 ```
 static CustomeCacheWithExpired()
{
    customeCache = new Dictionary<string, KeyValuePair<object, DateTime>>();
    Task.Run(() => 
    {
        while (true) 
        {
            List<string> expiredKeys = new List<string>();
            foreach (var key in customeCache.Keys) 
            {
                var cacheValue = customeCache[key];
                if (cacheValue.Value < DateTime.Now)
                {
                    expiredKeys.Add(key);
                }
            }
            expiredKeys.ForEach(key => { customeCache.Remove(key); });
        }
    });
}

 ```  


 1.3 并发情况加锁，或者用concurrency线程安全的类   
 多线程，要加锁  
 ```
 private static readonly object LockObject = new object();
private static void LockAction(Action action) 
{
    lock (LockObject) 
    {
        action.Invoke();
    }
}

// 添加,检查是否存在，都需要加
public static void Add(string key, object value) 
{
    LockAction(new Action(() => {
        if (customeCache.ContainsKey(key))
        {
            customeCache[key] = value;
        }
        else
        {
            customeCache.Add(key, value);
        }
    }));
}
 ```


# 2.Task


