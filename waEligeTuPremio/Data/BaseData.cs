using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace waEligeTuPremio.Data
{
    public partial class BaseData : IDisposable
    {


        #region members

        static string _staticConnectionString;
        bool _isDisposed = false;
        #endregion

        #region initialisation
        public BaseData()
        {
            Init();
        }

        private void Init()
        {
        }
        #endregion

        #region disposable interface support
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (!_isDisposed)
            {
                if (isDisposing)
                {

                }
            }
            _isDisposed = true;
        }
        #endregion

        #region connection support
        public static SqlConnection StaticSqlConnection
        {
            get
            {
                SqlConnection staticConnection = new SqlConnection();
                staticConnection.ConnectionString = StaticConnectionString;
                return staticConnection;
            }
        }



        public static string StaticConnectionString
        {
            set { _staticConnectionString = value; }
            get
            {
                if (!string.IsNullOrEmpty(_staticConnectionString))
                    return _staticConnectionString;

                return ConfigurationManager.ConnectionStrings["DBPremioConnectionString"].ToString();
            }
        }
        #endregion
    }
}
