using System;
using System.Collections.Generic;
using System.Linq;

namespace Talonario.Api.Server.Application.ViewModels
{
    public class PaginacaoViewModel<T>
    {
        #region Public Constructors

        public PaginacaoViewModel(IEnumerable<T> items, int totalItems, int? page = 0, int? limit = 0)
        {
            Items = items;
            TotalItems = totalItems;
            TotalPages = 0;

            if (totalItems == 0 && items != null && items.Count() > 0)
                TotalItems = items.Count();

            if (items != null && items.Count() > 0 && totalItems > 0)
            {
                if (limit > 0)
                    TotalPages = (int)Math.Ceiling(totalItems / (double)limit.Value);
                else
                    TotalPages = 1;

                CurrentPage = page > 0 ? page.Value : 1;
            }
        }

        #endregion Public Constructors

        #region Public Properties

        public int CurrentPage { get; set; } = 0;

        public IEnumerable<T> Items { get; set; }

        public int TotalItems { get; set; } = 0;

        public int TotalPages { get; set; } = 0;

        #endregion Public Properties
    }
}