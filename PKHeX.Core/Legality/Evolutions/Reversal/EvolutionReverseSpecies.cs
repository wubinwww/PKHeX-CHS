using System.Collections.Generic;

namespace PKHeX.Core;

/// <summary>
/// Provides functionality to reverse-engineer evolutionary relationships between species and forms.
/// </summary>
/// <remarks>This class is used to determine pre-evolutionary relationships for a given species and form,  as well
/// as to attempt devolving a Pokémon to its prior evolutionary state. It relies on an evolution method table and
/// personal data to construct a reverse lookup of evolutionary lineages.</remarks>
/// <param name="Entries">Collection of evolution methods indexed by species ID.</param>
/// <param name="Personal">Personal Table containing species and form data.</param>
/// <seealso cref="EvolutionReversePersonal"/>
public sealed class EvolutionReverseSpecies(EvolutionMethod[][] Entries, IPersonalTable Personal) : IEvolutionReverse
{
    public EvolutionReverseLookup Lineage { get; } = GetLineage(Personal, Entries);
    public ref readonly EvolutionNode GetReverse(ushort species, byte form) => ref Lineage[species, form];

    private static EvolutionReverseLookup GetLineage(IPersonalTable t, EvolutionMethod[][] entries)
    {
        var maxSpecies = t.MaxSpeciesID;
        var lineage = new EvolutionReverseLookup(maxSpecies);
        for (ushort sSpecies = 1; sSpecies <= maxSpecies; sSpecies++)
        {
            var fc = t[sSpecies].FormCount;
            for (byte sForm = 0; sForm < fc; sForm++)
            {
                foreach (var evo in entries[sSpecies])
                {
                    var dSpecies = evo.Species;
                    if (dSpecies == 0)
                        break;

                    var dForm = evo.GetDestinationForm(sForm);
                    var link = new EvolutionLink(evo, sSpecies, sForm);
                    lineage.Register(link, dSpecies, dForm);
                }
            }
        }

        return lineage;
    }

    public IEnumerable<(ushort Species, byte Form)> GetPreEvolutions(ushort species, byte form)
    {
        var node = Lineage[species, form];

        // No convergent evolutions; first method is enough.
        var s = node.First;
        if (s.Species == 0)
            yield break;

        var preEvolutions = GetPreEvolutions(s.Species, s.Form);
        foreach (var preEvo in preEvolutions)
            yield return preEvo;
        yield return (s.Species, s.Form);
    }

    public bool TryDevolve<T>(T head, PKM pk, byte currentMaxLevel, byte levelMin, bool skipChecks, EvolutionRuleTweak tweak, out EvoCriteria result)
        where T : ISpeciesForm
    {
        ref readonly var node = ref Lineage[head.Species, head.Form];
        return node.TryDevolve(pk, currentMaxLevel, levelMin, skipChecks, tweak, out result);
    }
}
