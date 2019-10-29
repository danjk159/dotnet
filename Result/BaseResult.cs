namespace TodoApi.Result {
    /// <summary>
    ///  返回基类
    /// </summary>
    public class BaseResult {
        /// <summary>
        ///  返回请求是否正常
        /// </summary>
        /// <value>0:正常,1:不正常,其它数值则还未定义</value>
        public string code { get; set; }
        /// <summary>
        /// 返回信息
        /// </summary>
        /// <value></value>
        public string msg { get; set; }
        /// <summary>
        /// 返回数据内容体
        /// </summary>
        /// <value></value>
        public object data { get; set; }
        /// <summary>
        /// 返回正常结果
        /// </summary>
        /// <param name="data">数据类型</param>
        /// <returns></returns>
        public static BaseResult getSuccessResult (object data) {
            BaseResult result = new BaseResult ();
            result.data = data;
            result.code = "0";
            result.msg = "获取成功";
            return result;
        }
        /// <summary>
        /// 返回异常结果
        /// </summary>
        /// <returns></returns>
        public static BaseResult getErrorResult () {
            BaseResult result = new BaseResult ();
            result.code = "1";
            result.msg = "请求失败,test";
            return result;
        }
    }
}