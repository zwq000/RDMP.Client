RMDP Client
=============

# 概述

RMDP Server 客户端程序示例

# 内容

## Nats.Subscribe  Nats订阅测试

Nats.Subscribe 基于 .net framework 4.5 构建,适用于 windowns 平台

构建成功后,执行以下命令
```cmd
Nats.Subscribe -l nats://xxx.xx.xx.xx:4222 -v -s rmdp
```

通过以下命令查看命令参数
```cmd
nats.subscribe.exe --help
```
## RMDP.Client.Nats RMDP Nats 订阅样例

程序完成 rmdp 主题订阅,并解析类型

```bash
dotnet restore
dotnet build
dotnet run -- -l nats://xxx.xx.xx.xx:4222 -s rmdp

```