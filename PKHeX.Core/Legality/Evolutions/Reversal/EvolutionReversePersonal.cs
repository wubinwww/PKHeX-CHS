using System.Collections.Generic;

namespace PKHeX.Core;

/// <summary>
/// Represents a reverse evolution lookup mechanism for determining the lineage of Pokémon species and forms.
/// </summary>
/// <remarks>This class provides functionality to trace the evolutionary lineage of a Pokémon species and form,
/// allowing reverse lookups to identify pre-evolutions and related evolutionary data. It is designed to work with a
/// personal table and a set of evolution method entries.</remarks>
/// <param name="Entries">Collection of evolution methods indexed by the Personal Table's <see cref="IPersonalTable.GetFormIndex"/></param>
/// <param name="Personal">Personal Table containing species and form data.</param>
/// <seealso cref="EvolutionReverseSpecies"/>
public sealed class EvolutionReversePersonal(EvolutionMethod[][] Entries, IPersonalTable Personal) : IEvolutionReverse
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
                var index = t.GetFormIndex(sSpecies, sForm);
                foreach (var evo in entries[index])
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
