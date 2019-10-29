namespace TodoApi.Models
{
/// <summary>
/// 测试使用
/// </summary>
    public class TodoItem
    {
      /// <summary>
      /// 主键id
      /// </summary>
      /// <value></value>
       
        public long Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        /// <value></value>
        public string Name { get; set; }
        /// <summary>
        ///  是否完成
        /// </summary>
        /// <value></value>
        public bool IsComplete { get; set; }
    }
}
