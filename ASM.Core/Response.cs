using System.Collections.Generic;
using System.Linq;

namespace ASM.Core
{
    public class Response : IResponse
    {
        #region Properties 

        public List<string> Failures
        {
            get;
            private set;
        }

        public string SuccessMessage
        {
            get;
            private set;
        }

        public ResponseState ResponseState
        {
            get { return Failures.Any() ? ResponseState.Failed : ResponseState.Success; }
        }

        #endregion

        #region Methods 

        #region Constructor 

        public Response()
        {
            Failures = new List<string>();
        }

        #endregion

        public void Fail(string failureMessage)
        {
            Failures.Add(failureMessage);
        }

        public static IResponse Create(List<IResponse> responses)
        {
            var response = responses.Aggregate();

            return string.IsNullOrEmpty(response)
                ? Success()
                : Failed(response);
        }

        public static IResponse Success()
        {
            return new Response();
        }

        public static IResponse Success(string successMessage)
        {
            return new Response { SuccessMessage = successMessage };
        }

        public static IResponse Failed(List<string> failures)
        {
            var response = new Response();
            failures.ForEach(response.Fail);

            return response;
        }

        public static IResponse Failed(string failure)
        {
            var response = new Response();
            response.Fail(failure);

            return response;
        }

        #endregion
    }

    public class Response<T> : Response, IResponse<T>
    {
        #region Properties 

        public T ResponseObject { get; private set; }

        #endregion

        #region Methods 

        #region Constructor 

        internal Response()
        { }

        #endregion

        public static IResponse<T> Success(T responseObject)
        {
            return new Response<T>
            {
                ResponseObject = responseObject
            };
        }

        public new static IResponse<T> Failed(string failure)
        {
            var response = new Response<T>();
            response.Fail(failure);

            return response;
        }

        #endregion
    }
}