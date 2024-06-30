using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Model
{
    public enum StockCalcLevel
    {

        /// <summary>
        /// 商家下载单量&发货单量日均(5000)
        /// </summary>
        [Description("5000")]
        Level5000 = 5000,


        /// <summary>
        /// 商家下载单量&发货单量日均(1000)
        /// </summary>
        [Description("1000")]
        Level1000 = 1000,


        /// <summary>
        /// Default 商家下载单量&发货单量日均(500)
        /// </summary>
        [Description("500")]
        Level500 = 500,


        /// <summary>
        /// 商家下载单量&发货单量日均(200)
        /// </summary>
        [Description("200")]
        Level200 = 200,


        /// <summary>
        /// 商家下载单量&发货单量日均(50)
        /// </summary>
        [Description("50")]
        Level50 = 50,


        /// <summary>
        /// 商家下载单量&发货单量日均(20)
        /// </summary>
        [Description("20")]
        Level20 = 20,


        /// <summary>
        /// 商家下载单量&发货单量日均(20)
        /// </summary>
        [Description("1")]
        Level1 = 1,


        [Description(">5000")]
        Level10000 = 10000,

        [Description("特殊的应用场景")]
        LevelMax = 999999

    }
}
