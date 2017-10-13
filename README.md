# TianCheng.SystemCommon
部门、用户、角色、功能点、菜单的通用处理

使用方式
-----------

在Startup.cs里面修改ConfigureServices 与 Configure 方法
```cs
public void ConfigureServices(IServiceCollection services)
{
	//原代码不用，直接用下面代码代替
    services.TianChengCommonInit(Configuration);
}

public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory,
    TianCheng.SystemCommon.Services.AuthService authService)
{
	//注意增加了一个authService的参数，验证的Service只要继承IAuthService即可
    app.TianChengCommonInit(Configuration, loggerFactory, authService);
}
```

数据库的链接配置
appsettings.json 文件中配置MongoDB数据库：
```cs
{
  "Logging": {
    "IncludeScopes": false,
    "LogLevel": {
      "Default": "Warning"
    }
  },
  "DatabaseInfo": {
    "ServerAddress": "mongodb://test:test@123.45.67.89:20000",
    "Database": "test"
  }
}

```