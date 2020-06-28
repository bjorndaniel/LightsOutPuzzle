// Copyright 2018-2019 Fabulous contributors. See LICENSE.md for license.
namespace LightsOutPuzzle.Fabulous

open Fabulous
open Fabulous.XamarinForms
open Fabulous.XamarinForms.LiveUpdate
open Xamarin.Forms
open LightsOutPuzzle.Common

module App = 
    type Model = 
      {
        IsActive: bool
        Game : Game
      }

    type Msg = 
        | Start
        | Reset
        | FlipTile

    let initModel = 
        {
            IsActive=false; 
            Game = Game.RandomGame() 
        }

    let init () = initModel, Cmd.none

    let update msg model =
        match msg with
           | Start -> { model with IsActive = true }, Cmd.none
           | Reset -> init ()
           | _ -> model, Cmd.none

    let view (model: Model) dispatch =
        View.ContentPage(
          content = View.StackLayout(
            padding = Thickness 20.0, 
            verticalOptions = LayoutOptions.Center,
            backgroundColor = Color.FromHex("#333"),
            children = [ 
                View.Label("A Fabulous for Xamarin forms game based on 'Lights Out' with a Marvel twist", 
                    margin = Thickness(20.0), 
                    textColor = Color.White,
                    horizontalTextAlignment = TextAlignment.Center, 
                    fontSize = FontSize(Device.GetNamedSize(NamedSize.Large, typeof<Label>)))
                View.Grid(
                    horizontalOptions = LayoutOptions.Center,
                    rowSpacing = 2.0,
                    columnSpacing = 2.0,
                    rowdefs = [for i in 1..5 -> Dimension.Absolute 50.0],
                    coldefs = [for i in 1..5 -> Dimension.Absolute 50.0],
                    children = [ for i in 1 .. 5 do for j in 1 .. 5 -> View.Image(source = ImageSrc (ImageSource.FromResource(sprintf "LightsOutPuzzle.Fabulous.Images.row-%i-col-%i.jpg" i j, typeof<Model>))).Row(i-1).Column(j-1)
                    ])
                                                  
                View.Button(text = (if model.IsActive then "Randomize & start over" else "Start game"), 
                    command = (fun () -> dispatch (if model.IsActive then Reset else Start)), 
                    backgroundColor = Color.FromHex("#007bff"),
                    textColor = Color.White,
                    horizontalOptions = LayoutOptions.Center)
                View.Label("Click on a tile to adjust the state of the tile and the direct adjacent ones. Keep on clicking to reveal the complete Marvel picture", 
                    margin = Thickness(20.0), 
                    textColor = Color.White,
                    horizontalTextAlignment = TextAlignment.Center) 
                View.Label("Created by Marcofolio.net based on the electronic game 'Lights Out' from 1995 by Tiger Electronics. Picture used from Marvels 'Avengers: Infinity War' 2018 movie.", 
                    margin = Thickness(20.0), 
                    textColor = Color.White,
                    horizontalTextAlignment = TextAlignment.Center,
                    fontSize = FontSize(Device.GetNamedSize(NamedSize.Micro, typeof<Label>))) 
            ]),
            
            backgroundColor = Color.FromHex("#333"))

    // Note, this declaration is needed if you enable LiveUpdate
    let program = XamarinFormsProgram.mkProgram init update view

type App () as app = 
    inherit Application ()

    let runner = 
        App.program
#if DEBUG
        |> Program.withConsoleTrace
#endif
        |> XamarinFormsProgram.run app

#if DEBUG
    // Uncomment this line to enable live update in debug mode. 
    // See https://fsprojects.github.io/Fabulous/Fabulous.XamarinForms/tools.html#live-update for further  instructions.
    do runner.EnableLiveUpdate()
#endif    

    // Uncomment this code to save the application state to app.Properties using Newtonsoft.Json
    // See https://fsprojects.github.io/Fabulous/Fabulous.XamarinForms/models.html#saving-application-state for further  instructions.
#if APPSAVE
    let modelId = "model"
    override __.OnSleep() = 

        let json = Newtonsoft.Json.JsonConvert.SerializeObject(runner.CurrentModel)
        Console.WriteLine("OnSleep: saving model into app.Properties, json = {0}", json)

        app.Properties.[modelId] <- json

    override __.OnResume() = 
        Console.WriteLine "OnResume: checking for model in app.Properties"
        try 
            match app.Properties.TryGetValue modelId with
            | true, (:? string as json) -> 

                Console.WriteLine("OnResume: restoring model from app.Properties, json = {0}", json)
                let model = Newtonsoft.Json.JsonConvert.DeserializeObject<App.Model>(json)

                Console.WriteLine("OnResume: restoring model from app.Properties, model = {0}", (sprintf "%0A" model))
                runner.SetCurrentModel (model, Cmd.none)

            | _ -> ()
        with ex -> 
            App.program.onError("Error while restoring model found in app.Properties", ex)

    override this.OnStart() = 
        Console.WriteLine "OnStart: using same logic as OnResume()"
        this.OnResume()
#endif


