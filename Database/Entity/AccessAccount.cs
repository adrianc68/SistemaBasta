
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------


namespace Database.Entity
{

using System;
    using System.Collections.Generic;
    
public partial class AccessAccount
{

    public string email { get; set; }

    public string username { get; set; }

    public string password { get; set; }

    public string recovery_code { get; set; }

    public AccountState account_state { get; set; }



    public virtual Player Player { get; set; }

}

}
