//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AndrK.ZavPostav.MsSqlRepository
{
    using System;
    using System.Collections.Generic;
    
    public partial class ZavkaZakup_Oborudovanie
    {
        public System.Guid id { get; set; }
        public System.Guid zavkaZakup_id { get; set; }
        public System.Guid oborudovanie_id { get; set; }
        public decimal kol { get; set; }
    
        public virtual Oborudovanie Oborudovanie { get; set; }
        public virtual ZavkaZakup ZavkaZakup { get; set; }
    }
}
