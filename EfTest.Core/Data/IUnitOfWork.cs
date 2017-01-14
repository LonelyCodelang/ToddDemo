using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfTest.Core.Data
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    ///     <para>    Creator：helang</para>
    ///     <para>CreatedTime：2017/1/12 23:24:50</para>
    /// </remarks>
    public interface IUnitOfWork
    {
        #region 属性

        /// <summary>
        /// 获取或设置 是否开启事务提交
        /// </summary>
        bool TransactionEnabled { get; set; }

        #endregion

        #region 方法

        /// <summary>
        /// 提交当前单元操作的更改。
        /// </summary>
        /// <returns>操作影响的行数</returns>
        int SaveChanges();

#if NET45

        /// <summary>
        /// 异步提交当前单元操作的更改。
        /// </summary>
        /// <returns>操作影响的行数</returns>
        Task<int> SaveChangesAsync();

#endif

        #endregion
    }
}
