namespace PKHeX.Core;

/// <summary>
/// Implements an external legality check that can be used to verify <see cref="PKM"/> legality.
/// </summary>
/// <remarks>
/// Usage:
/// - register in <see cref="ExternalLegalityCheck"/>.
/// - for any generated <see cref="CheckResult"/>, tag as <see cref="LegalityCheckResultCode.External"/>,
///   with the <see cref="CheckResult.Argument"/> set to the <see cref="Identity"/> of this checker.
///   You can still use <see cref="CheckResult.Argument2"/> to store values useful for localization, if you must.
/// </remarks>
public interface IExternalLegalityChecker : ILegalityVerifier
{
    /// <summary>
    /// Friendly name of the legality check, used for display/internal purposes.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Unique identity of the legality check, used to identify the check in results.
    /// </summary>
    public ushort Identity { get; }

    /// <summary>
    /// Requests a localized string for the given check result.
    /// </summary>
    /// <param name="chk">Check result to localize.</param>
    /// <param name="settings">Localization settings and strings that can be used.</param>
    /// <param name="data">Analysis the check originated from, which may contain additional context.</param>
    /// <returns>Localized string for the check result.</returns>
    string Localize(CheckResult chk, LegalityLocalizationSet settings, LegalityAnalysis data);
}
