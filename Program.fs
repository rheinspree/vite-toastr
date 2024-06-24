module App

open Feliz
open Feliz.UseElmish
open Elmish
open Elmish.Toastr

module Counter =
    type Model =
        {
            count : int
        }
    let init() =
        {
            count = 0
        }
        , Cmd.none
    type Msg =
        | Increment
        | Decrement
    let update msg model =
        
        let showToastCmd (msg: string) =
            Toastr.message msg
            |> Toastr.position TopRight
            |> Toastr.timeout 3000
            |> Toastr.withProgressBar
            |> Toastr.hideEasing Easing.Swing
            |> Toastr.success
        
        match msg with
        | Increment ->
            { model with
                count = model.count + 1
            }
            , showToastCmd $"Counter: {model.count}"
        | Decrement ->
            { model with
                count = model.count - 1
            }
            , showToastCmd $"Counter: {model.count}"
            
    [<ReactComponent>]
    let View () =
        let _, dispatch = React.useElmish(init, update, [| |])
        Html.div [
            Html.button [
                prop.text "Decrement"
                prop.onClick (fun _ -> Decrement |> dispatch)
            ]
            Html.button [
                prop.text "Increment"
                prop.onClick (fun _ -> Increment |> dispatch)
            ]
        ]

(ReactDOM.createRoot (Browser.Dom.document.getElementById "root")).render (Counter.View ())