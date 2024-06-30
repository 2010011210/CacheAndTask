# CacheAndTask
Cache and task demo 2024-06-30     

# 1.Cache   
 1.1 使用委托传入默认值   
  ```
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
 1.3 并发情况加锁，或者用concurrency线程安全的类


# 2.Task


