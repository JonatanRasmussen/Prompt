using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace GlobalNameSpace;

public static class MainPage
{
    public static Prompt CreateNewPrompt()
    {
        return new();
    }
}