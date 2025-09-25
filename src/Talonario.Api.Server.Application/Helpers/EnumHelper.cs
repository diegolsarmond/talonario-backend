using System.ComponentModel;

namespace Talonario.Api.Server.Application.Helpers
{
    public static class EnumHelper
    {
        #region Public Methods

        public static string GetEnumDescription<T>(T enumItem)
        {
            var description = enumItem.ToString();
            var fieldInfo = enumItem.GetType().GetField(enumItem.ToString());

            if (fieldInfo != null)
            {
                var attributes = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attributes != null && attributes.Length > 0)
                    description = ((DescriptionAttribute)attributes[0]).Description;
            }

            return description;
        }

        #endregion Public Methods
    }
}