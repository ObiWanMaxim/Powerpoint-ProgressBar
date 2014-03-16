﻿using ProgressBar.Bar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgressBar.BuiltInPresentation
{
    class StrippedBar : IBar
    {
        private PresentationInfo presentationInfo;
        private PositionOptions p;

        public StrippedBar()
        {
            this.p = new PositionOptions();
            this.p.Bottom = this.p.Left = this.p.Right = this.p.Top = true;
        }

        public PositionOptions GetPositionOptions()
        {
            return this.p;
        }

        public List<Microsoft.Office.Interop.PowerPoint.Shape> Render(int currentPosition, PresentationInfo ppp)
        {
            throw new NotImplementedException();
        }

        private IBasicShape MakeShapeStub(PresentationInfo presentation)
        {
            IBasicShape shapeStub = new BasicShape();

            shapeStub.Height = presentation.UserSize;
            shapeStub.Top = 0;
            shapeStub.Left = 0;
            shapeStub.Type = Microsoft.Office.Core.MsoAutoShapeType.msoShapeRectangle;

            return shapeStub;
        }

        private IBasicShape MakeBackground()
        {
            IBasicShape backgroundShape = this.MakeShapeStub(this.presentationInfo);

            backgroundShape.Width = this.presentationInfo.Width;
            backgroundShape.ColorType = DataStructs.ShapeType.BACKGROUND;

            return backgroundShape;
        }

        private IBasicShape MakeProgressBar(int currentPosition)
        {
            IBasicShape backgroundShape = this.MakeShapeStub(this.presentationInfo);

            backgroundShape.Width = (this.presentationInfo.Width / this.presentationInfo.SlidesCount) * currentPosition;
            backgroundShape.ColorType = DataStructs.ShapeType.PROGRESS_BAR;

            return backgroundShape;
        }


        List<IBasicShape> IBar.Render(int currentPosition, PresentationInfo presentationInfo)
        {
            this.presentationInfo = presentationInfo;

            List<IBasicShape> shapes = new List<IBasicShape>();

            shapes.Add(this.MakeBackground());
            shapes.Add(this.MakeProgressBar(currentPosition));

            return shapes;
        }
    }
}
