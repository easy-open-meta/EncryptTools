using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yitter.IdGenerator;

namespace LockedUtil
{
    public class Encrypt
    {
        static short method = 1; // 1-漂移算法，2-传统算法

        /// <summary>
        /// 利用雪花漂移算法生成对应的密钥Key
        /// </summary>
        /// <returns></returns>
        public long Snow_Alg()
        {
            var options = new IdGeneratorOptions()
            {
                //算法类型，1、雪花算法2、传统算法
                Method = method,
                //机器码，与 WorkerIdBitLength 有关系
                WorkerId = 60,
                //基础时间，不能超过当前时间
                BaseTime = DateTime.Now.AddDays(-1),
                //机器码位长，建议6-12，与序列数位长相加不超过22
                WorkerIdBitLength = 7,
                //序列数位长,建议6-14
                SeqBitLength = 6,
                //最大漂移次数（含），默认2000，推荐范围500-10000（与计算能力有关）
                TopOverCostCount = 1500,
                MaxSeqNumber = 10,
                MinSeqNumber = 10
            };
            YitIdHelper.SetIdGenerator(options);

            var result = YitIdHelper.NextId();

            return result;
        }

        LockedHelper lockedHelper = new LockedHelper();
        
        /// <summary>
        /// 加密方法
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string EncryptStr(string str)
        {
            string key = "";
            key = Snow_Alg().ToString();
            string result = string.Empty;
            result = lockedHelper.EncryptStr(str, 3, "-");
            string newStr = lockedHelper.RSAEncrypt(result, key);
            return newStr;
        }

        /// <summary>
        /// 解密方法
        /// </summary>
        /// <param name="newStr"></param>
        /// <returns></returns>
        public string DeEncryptStr(string newStr)
        {
            LockedHelper lockedHelper = new LockedHelper();
            string subKey = newStr.Split('k', 'e', 'y', ':').LastOrDefault();
            string sourceString = newStr.Split(':').FirstOrDefault();
            string NewDeStr = lockedHelper.RSADecrypt(sourceString, subKey);
            string sourceStr = NewDeStr.Replace("-", string.Empty).ToString();
            return sourceStr;
        }

    }
}
