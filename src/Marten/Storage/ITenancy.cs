﻿using System.Threading.Tasks;
using Marten.Exceptions;
using Marten.Schema;
using Weasel.Core.Migrations;

namespace Marten.Storage;

#region sample_ITenancy

/// <summary>
///     Pluggable interface for Marten multi-tenancy by database
/// </summary>
public interface ITenancy: IDatabaseSource
{
    /// <summary>
    ///     The default tenant. This can be null.
    /// </summary>
    Tenant Default { get; }

    /// <summary>
    ///     A composite document cleaner for the entire collection of databases
    /// </summary>
    IDocumentCleaner Cleaner { get; }

    /// <summary>
    ///     Retrieve or create a Tenant for the tenant id.
    /// </summary>
    /// <param name="tenantId"></param>
    /// <exception cref="UnknownTenantIdException"></exception>
    /// <returns></returns>
    Tenant GetTenant(string tenantId);

    /// <summary>
    ///     Retrieve or create a tenant for the tenant id
    /// </summary>
    /// <param name="tenantId"></param>
    /// <returns></returns>
    ValueTask<Tenant> GetTenantAsync(string tenantId);

    /// <summary>
    ///     Find or create the named database
    /// </summary>
    /// <param name="tenantIdOrDatabaseIdentifier"></param>
    /// <returns></returns>
    ValueTask<IMartenDatabase> FindOrCreateDatabase(string tenantIdOrDatabaseIdentifier);

    /// <summary>
    ///  Asserts that the requested tenant id is part of the current database
    /// </summary>
    /// <param name="database"></param>
    /// <param name="tenantId"></param>
    bool IsTenantStoredInCurrentDatabase(IMartenDatabase database, string tenantId);
}

#endregion

public class UnknownTenantIdException: MartenException
{
    public UnknownTenantIdException(string tenantId): base($"Unknown tenant id '{tenantId}'")
    {
    }
}
