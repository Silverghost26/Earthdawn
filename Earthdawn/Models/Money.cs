using System;
using System.Collections.Generic;

namespace Earthdawn.Models;

public class Money
{
    public Money()
    {
        _gems = new List<Gem>();
    }

    //Copy constructor.
    public Money(Money money)
    {
    }
    //Private variables
    private List<Gem> _gems;
    
    //Properties.
    public int Silver { get; set; }
    public int Gold { get; set; }   
    public int Copper { get; set; }

    //Functions
    public void SellGem(Guid gemId)
    {
        if (_gems == null)
            return;
        foreach (var gem in _gems)
        {
            if (gem.Identifier == gemId)
            {
                Silver += gem.SilverValue;
                _gems.Remove(gem);
            }
        }
    }

    public void AddGem(int gemValue, string gemType, string description, bool appraised)
    {
        if (_gems == null)
            return;
        _gems.Add(new Gem(gemValue, gemType, description, appraised));
    }

    public Gem LooseGem(Guid gemId)
    {
        if (_gems == null)
            return null;
        foreach (var gem in _gems)
        {
            if (gem.Identifier == gemId)
                _gems.Remove(gem);
            return gem;
        }

        return null;
    }

    public void SpendGold(int costInGold)
    {
        if (Gold < costInGold)
            return;
        
    }

    public bool SpendSilver(int costInSilver)
    {
        if (Silver < costInSilver)
            return false;
        
        Silver -= costInSilver;
        return true;
    }
    
    //Private fuctions.
    private void _ConvertSilverToGold(int silver)
    {
        if (silver < Silver)
            return;
        Silver += (silver % 10);
        Gold += (silver / 10);
        Silver -= silver;
    }

    private void _ConvertSilverToCopper(int silver)
    {
        if (silver < Silver)
            return;
        Copper += (silver * 10);
        Silver -= silver;
    }

    private void _ConvertGoldToSilver(int gold)
    {
        if (gold < Gold)
            return;
        Silver += (gold * 10);
        Gold -= gold;
    }
    private void _ConvertCopperToSilver(int copper)
    {
        if (copper < Copper)
            return;
        Copper += (copper % 10);
        Silver += (copper / 10);
        Copper -= copper;
    }
}

public class Gem
{
    public Gem(int value, string gemType, string description, bool appraised)
    {
        _identifier = Guid.NewGuid();
        SilverValue = value;
        Description = description;
        Appraised = appraised;
        GemType = gemType;
    }
        
    //Copy constuctor
    public Gem(Gem gem)
    {
        _identifier = gem._identifier;
        SilverValue = gem.SilverValue;
        Description = gem.Description;
        Appraised = gem.Appraised;
        GemType = gem.GemType;
    }
        
    //private vars
    private readonly Guid _identifier;
        
    //Properties.
    public Guid Identifier
    {
        get => _identifier;
    }

    public int SilverValue { get; set; }
    public string Description { get; set; }
    public bool Appraised { get; set; }
    public string GemType { get; set; }
}

