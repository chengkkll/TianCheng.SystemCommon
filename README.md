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

public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    app.TianChengCommonInit(Configuration);
}
```

配置appsettings.json说明
1. 配置Swagger
2. 设置登录验证的Token信息
3. 配置MongoDB数据库连接
4. FunctionModule可配置，也可不配置，此配置主要用于权限中功能点的自动生成处理

```cs
{
  "Logging": {
    "LogLevel": {
      "Default": "Warning"
    }
  },
  "AllowedHosts": "*",
  "SwaggerDoc": {
    "Version": "v1",
    "Title": "demo",
    "Description": "RESTful API for demo",
    "TermsOfService": "None",
    "ContactName": "cheng_kkll",
    "ContactEmail": "cheng_kkll@163.com",
    "ContactUrl": "",
    "EndpointUrl": "/swagger/v1/swagger.json",
    "EndpointDesc": "demo API V1"
  },
  "Token": {
    "Issuer": "demo.issuer",
    "Audience": "demo.audi",
    "SecretKey": "DemoSecretKey",
    "Scheme": "Bearer",
    "Path": "/api/auth/login"
  },  
  "FunctionModule": {
    "ModuleDict": [
      {
        "Code": "TianCheng.SystemCommon",
        "Name": "系统管理"
      }
    ]
  },
  "DatabaseInfo": {
    "ServerAddress": "mongodb://demo:demo@loalhost",
    "Database": "demo"
  }
}

```