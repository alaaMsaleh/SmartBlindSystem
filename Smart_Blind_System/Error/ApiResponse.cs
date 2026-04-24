namespace Smart_Blind_System.API.Error
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }

        public ApiResponse(int statusCode, string? message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessage(StatusCode);
        }

        private string? GetDefaultMessage(int statusCode)
        {
            //Switch Expression 

            return statusCode switch
            {
                400 => "A Bad Request",
                401 => "Authorized , you are not",
                404 => "Resource was not fount :(",
                500 => "Error are the Path to dark side , Errors lead to anger , anger leads to hate, Hate leads Career Change!!!",
                _ => null
            };
        }
    }
}
