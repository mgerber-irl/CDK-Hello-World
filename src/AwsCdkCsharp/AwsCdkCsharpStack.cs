using Amazon.CDK;
using Amazon.CDK.AWS.APIGateway;
using Amazon.CDK.AWS.Lambda;
using Constructs;

namespace AwsCdkCsharp
{
    public class AwsCdkCsharpStack : Stack
    {
        internal AwsCdkCsharpStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
            // Lambda function
            var helloWorldFunction = new Function(this, "HelloWorldFunction", new FunctionProps
            {
                Runtime = Runtime.NODEJS_20_X,
                Code = Code.FromAsset("lambda"),
                Handler = "hello.handler"
            });

            // API Gateway
            var api = new LambdaRestApi(this, "HelloWorldApi", new LambdaRestApiProps
            {
                Handler = helloWorldFunction,
                Proxy = false
            });

            // Resource
            var hello = api.Root.AddResource("hello");
            hello.AddMethod("GET");

            // Output
            new CfnOutput(this, "HelloWorldApiUrl", new CfnOutputProps
            {
                Value = $"{api.Url}hello"
            });
        }
    }
}
