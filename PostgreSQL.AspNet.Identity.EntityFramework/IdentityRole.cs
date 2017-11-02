﻿using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PostgreSQL.AspNet.Identity.EntityFramework {

	/// <summary>
	/// Represents a Role entity
	/// </summary>
	public class IdentityRole : IdentityRole<string, IdentityUserRole> {

		/// <summary>
		/// Constructor
		/// </summary>
		public IdentityRole() {
			Id = Guid.NewGuid().ToString();
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="roleName"></param>
		public IdentityRole(string roleName)
			: this() {
			Name = roleName;
		}
	}

	/// <summary>
	/// Represents a Role entity
	/// </summary>
	/// <typeparam name="TKey"></typeparam>
	/// <typeparam name="TUserRole"></typeparam>
	public class IdentityRole<TKey, TUserRole> : IRole<TKey> where TUserRole : IdentityUserRole<TKey> {

		/// <summary>
		/// Constructor
		/// </summary>
		public IdentityRole() {
			Users = new List<TUserRole>();
		}

		/// <summary>
		/// Role id
		/// </summary>
		public TKey Id { get; set; }

        /// <summary>
        /// Role name
        /// </summary>
        [Display(Name = "Nom du rôle")]
        public string Name { get; set; }

		/// <summary>
		/// Navigation property for users in the role
		/// </summary>
		public virtual ICollection<TUserRole> Users { get; private set; }
	}
}
