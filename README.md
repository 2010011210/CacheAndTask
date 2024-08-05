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

## 2.1 Thread.Sleep(1000)和Task.Delay(1000)  
Thread.Sleep是阻塞的，要等Thread.Sleep等待的秒数够了之后，才会执行后面的。   
Task.Delay是非阻塞的，会直接跳过Task.Delay执行后面的代码。Task.Delay通常和ContinueWith一起使用。
~~~
Task.Delay(5000).ContinueWith((task) => { 
    Console.WriteLine("延迟12秒执行");
    Task.Run(() => { Utility.DoSomething(); });
});   // 延迟
~~~
Task.Delay主要是实现异步执行，如果采用阻塞的，没有意义。同样在一部任务中使用Sleep也没有意义。

## 2.2 Task.WaitAll, Task.WaitAny,Task.WhenAll,Task.WhenAny
WaitAll和WaitAny是阻塞的，要等待全部task或任意一个执行完成才会继续向下执行。  
WhenAll和WhenAny是非阻塞的，通常是异步任务，与ContinueWith一起使用。
~~~
List<Task<int>> tasks = new List<Task<int>>();
tasks.Add(Task.Run(() => Utility.DoSomething(1)));
tasks.Add(Task.Run(() => Utility.DoSomething(2)));
tasks.Add(Task.Run(() => Utility.DoSomething(3)));
tasks.Add(Task.Run(() => Utility.DoSomething(4)));
var result1 = Task.WaitAny(tasks.ToArray());
Console.WriteLine($"任意一个完成了:{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}，ThreadId:{Thread.CurrentThread.ManagedThreadId}");

Task.WaitAll(tasks.ToArray());
Console.WriteLine($"WaitAll所有Task都完成了:{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}，ThreadId:{Thread.CurrentThread.ManagedThreadId}");

var result2 =  Task.WhenAll(tasks.ToArray()).ContinueWith(t =>
    Console.WriteLine($"所有Task完成后执行:{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}，ThreadId:{Thread.CurrentThread.ManagedThreadId}")
);

var result3 = Task.WhenAny(tasks.ToArray()).ContinueWith(t =>
    Console.WriteLine($"任意Task完成后执行:{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}，ThreadId:{Thread.CurrentThread.ManagedThreadId}")
);

var result4 = Task.WhenAll(tasks.ToArray()).GetAwaiter().GetResult();  //可以通过这个实现同步执行，等待完成后再向下走。

public  class Utility
{
    public static void DoSomething() 
    {
        Console.WriteLine($"Doing something no sleep:{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}，ThreadId:{Thread.CurrentThread.ManagedThreadId}");
    }

    public static int DoSomething(int sleep)
    {
        Thread.Sleep(sleep * 1000);
        Console.WriteLine($"Doing something:{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}，ThreadId:{Thread.CurrentThread.ManagedThreadId}");
        return sleep;
    }
}
~~~

WhenAll也可以通过GetWaitor.GetResult()实现同步执行。
~~~
var result4 = Task.WhenAll(tasks.ToArray()).GetAwaiter().GetResult();
~~~

## 2.2 Parallel类   
ParallelOptions中MaxDegreeOfParallelism是最大线程数量
~~~
Parallel.ForEach(listIds, new ParallelOptions() { MaxDegreeOfParallelism = 10 }, inventory =>
{
    listId2.Add(inventory.ToString());
    notExistShopSkuIds.Add(inventory);

});
~~~



