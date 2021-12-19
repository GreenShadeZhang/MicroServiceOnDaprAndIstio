# MicroServiceOnDaprAndIstio
## 测试微服务在dapr下的服务互相调用，包括从http到grpc，http到http，http到grpc到grpc，后期会加入istio和dapr的配合使用。

## 目前的测试项目为两个http服务和两个grpc服务 

## 通过测试在http服务调用http再调用grpc证明了grpc只和边车(sidecar)建立连接,客户端是http调用dapr的时候委托给dpar所以可以在http服务里自由调用http和grpc。

## Grpc调用http也没问题，边车已处理相关问题。
