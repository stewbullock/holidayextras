using System.Collections.Generic;

namespace ASM.Core
{
    public interface IResponse
    {
        List<string> Failures { get; }
        string SuccessMessage { get; }
        ResponseState ResponseState { get; }
    }

    public interface IResponse<out T> : IResponse
    {
        T ResponseObject { get; }
    }
}