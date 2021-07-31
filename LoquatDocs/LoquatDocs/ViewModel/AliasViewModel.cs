﻿using LoquatDocs.EntityFramework;
using LoquatDocs.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoquatDocs.ViewModel {
  public class AliasViewModel : ObservableObject {
    private const string RESOURCE_KEY = "Alias";

    private Alias _alias = new Alias();

    public string Synonym {
      get => _alias.Synonym;
      set => SetProperty(ref _alias.Synonym, value);
    }

    public string Word {
      get => _alias.Word;
      set => SetProperty(ref _alias.Word, value);
    }

    public IAsyncRelayCommand SaveCommand { get; }

    public AliasViewModel() {
      SaveCommand = new AsyncRelayCommand(SaveAsync);
    }

    private async Task SaveAsync() {
      if (await DoesWordExistAsGroup())
      using(LoquatDocsDbContext ctx = new LoquatDocsDbContext()) {

      }
    }

    private bool IsWordsValidToSave() {
      return Synonym is not null && Word is not null && Synonym != Word;
    }

    private async Task<bool> DoesWordExistAsGroup() {
      using (LoquatDocsDbContext ctx = new LoquatDocsDbContext()) {
        return await ctx.AliasGroups.AnyAsync(group => group.AliasGroupName.Equals(Word));
      }
    }
  }
}