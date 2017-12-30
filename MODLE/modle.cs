using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MODLE
{
    public class IpAdr//IP地址
    {
        public string IP { get; set; }  //地址
        public string Port { get; set; }  //端口
        public int check { get; set; }  //0 未检查 1有效  2无效
    }
}
