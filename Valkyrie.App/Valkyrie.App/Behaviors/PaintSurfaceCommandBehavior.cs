/*=========================================================================
 * 
 * Adam Coville
 * adam.coville@gmail.com
 * 
 * Upskilled ICT401515 / Core Infrastructure Mobile Project
 * 
 * Credit to 
 * Matthew Leibowitz
 * .NET Development Addict
 * 
 * https://dotnetdevaddict.co.za/2017/07/13/turning-events-into-commands/
 * 
 * adapted to use an SKGLView instead of an SKCanvasView
 * 
 * =======================================================================*/

using SkiaSharp.Views.Forms;
using System;
using System.Reflection;
using System.Windows.Input;
using Xamarin.Forms;

namespace Valkyrie.App.Behaviors
{
    public class PaintSurfaceCommandBehavior : BehaviorBase<SKGLView>
    {
        Delegate eventHandler;

        //=====================================================================

        public string EventName
        {
            get
            {
                return (string)GetValue(EventNameProperty);
            }
            set
            {
                SetValue(EventNameProperty, value);
            }
        }

        //=====================================================================

        public static readonly BindableProperty EventNameProperty = BindableProperty.Create
            ("EventName",
             typeof(string),
             typeof(PaintSurfaceCommandBehavior),
             null,
             propertyChanged: OnEventNameChanged);

        //=====================================================================

        //-- bindable property for the command

        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(
                nameof(Command),
                typeof(ICommand),
                typeof(PaintSurfaceCommandBehavior),
                null);

        //======================================================================

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create
            ("CommandParameter",
             typeof(object),
             typeof(EventToCommandBehavior),
             null);

        //======================================================================

        public static readonly BindableProperty InputConverterProperty = BindableProperty.Create
            ("Converter",
             typeof(IValueConverter),
             typeof(EventToCommandBehavior),
             null);

        //=====================================================================

        //-- the command property

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        //====================================================================

        public object CommandParameter
        {
            get
            {
                return GetValue(CommandParameterProperty);
            }
            set
            {
                SetValue(CommandParameterProperty, value);
            }
        }

        //======================================================================

        public IValueConverter Converter
        {
            get
            {
                return (IValueConverter)GetValue(InputConverterProperty);
            }

            set
            {
                SetValue(InputConverterProperty, value);
            }
        }

        //=============================================================

        void RegisterEvent(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return;
            }

            EventInfo eventInfo = AssociatedObject.GetType().GetRuntimeEvent(name);

            if (eventInfo == null)
            {
                throw new ArgumentException(string.Format("EventToCommandBehavior: Can't register the '{0}' event.", EventName));
            }

            MethodInfo methodInfo = typeof(EventToCommandBehavior).GetTypeInfo().GetDeclaredMethod("OnEvent");

            eventHandler = methodInfo.CreateDelegate(eventInfo.EventHandlerType, this);

            eventInfo.AddEventHandler(AssociatedObject, eventHandler);
        }

        //=============================================================

        void DeregisterEvent(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return;
            }

            if (eventHandler == null)
            {
                return;
            }

            EventInfo eventInfo = AssociatedObject.GetType().GetRuntimeEvent(name);

            if (eventInfo == null)
            {
                throw new ArgumentException(string.Format("EventToCommandBehavior: Can't de-register the '{0}' event.", EventName));
            }

            eventInfo.RemoveEventHandler(AssociatedObject, eventHandler);
            eventHandler = null;
        }

        //=============================================================

        void OnEvent(object sender, object eventArgs)
        {
            if (Command == null)
            {
                return;
            }

            object resolvedParameter;

            if (CommandParameter != null)
            {
                resolvedParameter = CommandParameter;
            }

            else if (Converter != null)
            {
                resolvedParameter = Converter.Convert(eventArgs, typeof(object), null, null);
            }

            else
            {
                resolvedParameter = eventArgs;
            }

            if (Command.CanExecute(resolvedParameter))
            {
                Command.Execute(resolvedParameter);
            }
        }

        //=====================================================================

        //-- invoked immediately after the behavior is attached to a control

        //protected override void OnAttachedTo(SKCanvasView bindable)

        protected override void OnAttachedTo(SKGLView bindable)
        {
            base.OnAttachedTo(bindable);

            // we want to be notified when the view's context changes

            bindable.BindingContextChanged += OnBindingContextChanged;

            // we are interested in the paint event

            bindable.PaintSurface += OnPaintSurface;
        }

        //=============================================================

        static void OnEventNameChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var behavior = (PaintSurfaceCommandBehavior)bindable;

            if (behavior.AssociatedObject == null)
            {
                return;
            }

            string oldEventName = (string)oldValue;
            string newEventName = (string)newValue;

            behavior.DeregisterEvent(oldEventName);
            behavior.RegisterEvent(newEventName);
        }

        //=====================================================================

        // invoked when the behavior is removed from the control 

        //protected override void OnDetachingFrom(SKCanvasView bindable)

        protected override void OnDetachingFrom(SKGLView bindable)
        {
            base.OnDetachingFrom(bindable);

            // unsurbscribe from all events

            bindable.BindingContextChanged -= OnBindingContextChanged;
            bindable.PaintSurface -= OnPaintSurface;
        }

        //=====================================================================

        // the view's context changed

        private void OnBindingContextChanged(object sender, EventArgs e)
        {
            // update the behavior's context to match the view

            BindingContext = ((BindableObject)sender).BindingContext;
        }

        //=====================================================================

        /*----------------------------------------------------------------
         * 
         * SKGLView.PaintSurface Event
         * https://docs.microsoft.com/en-us/dotnet/api/skiasharp.views.forms.skglview.paintsurface?view=skiasharp-views-forms-1.68.1
         * 
         * There are 2 ways to draw on this surface: 
         * by overridingthe OnPaintSurface(SKPaintGLSurfaceVentArgs)
         * method, or by attaching a handler to the PaintSurface event
         * 
         * -----------------------------------------------------------*/

        private void OnPaintSurface(object sender, SKPaintGLSurfaceEventArgs e)
        {
            var surface = e.Surface;
            var surfaceWidth = e.BackendRenderTarget.Width;
            var surfaceHeight = e.BackendRenderTarget.Height;

            var canvas = surface.Canvas;

            // draw on the canvas

            canvas.Flush();
        }
    }
}