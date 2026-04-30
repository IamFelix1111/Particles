using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Particles
{
    public partial class FrmParticles : Form
    {
        #region 核心配置
        private class ParticleConfig
        {
            public int ParticlesCount { get; set; } = 125;
            public bool DensityEnable { get; set; } = true;
            public int DensityArea { get; set; } = 800;
            public Color ParticleColor { get; set; } = Color.FromArgb(16, 124, 16);
            public string ShapeType { get; set; } = "circle";
            public int ShapeSides { get; set; } = 5;
            public float Opacity { get; set; } = 0.9f;
            public bool OpacityRandom { get; set; } = true;
            public bool OpacityAnim { get; set; } = true;
            public float OpacityAnimSpeed { get; set; } = 1f;
            public float OpacityMin { get; set; } = 1f;
            public bool OpacitySync { get; set; } = false;

            public float Size { get; set; } = 3f;
            public bool SizeRandom { get; set; } = true;
            public bool SizeAnim { get; set; } = true;
            public float SizeAnimSpeed { get; set; } = 2f;
            public float SizeMin { get; set; } = 1f;
            public bool SizeSync { get; set; } = false;

            public bool LineLinked { get; set; } = true;
            public float LineDistance { get; set; } = 125f;
            public Color LineColor { get; set; } = Color.FromArgb(16, 160, 16);
            public float LineOpacity { get; set; } = 0.8f;
            public float LineWidth { get; set; } = 2f;

            public bool MoveEnable { get; set; } = true;
            public float MoveSpeed { get; set; } = 1.5f;
            public string MoveDirection { get; set; } = "none";
            public bool MoveRandom { get; set; } = false;
            public bool MoveStraight { get; set; } = false;
            public string OutMode { get; set; } = "out";

            public bool HoverEnable { get; set; } = true;
            public string HoverMode { get; set; } = "grab,bubble";
            public bool ClickEnable { get; set; } = true;
            public string ClickMode { get; set; } = "repulse";

            public float GrabDistance { get; set; } = 150f;
            public float GrabLineOpacity { get; set; } = 0.8f;

            public float BubbleDistance { get; set; } = 200f;
            public float BubbleSize { get; set; } = 5f;
            public float BubbleDuration { get; set; } = 2f;
            public float BubbleOpacity { get; set; } = 0.8f;

            public float RepulseDistance { get; set; } = 150f;
            public float RepulseDuration { get; set; } = 0.8f;

            public int PushCount { get; set; } = 4;
            public int RemoveCount { get; set; } = 4;

            public bool RetinaDetect { get; set; } = true;
        }

        private class Particle
        {
            public float X { get; set; }
            public float Y { get; set; }
            public float Radius { get; set; }
            public float RadiusBubble { get; set; }
            public float Opacity { get; set; }
            public float OpacityBubble { get; set; }
            public Color Color { get; set; }
            public float Vx { get; set; }
            public float Vy { get; set; }
            public float VxInit { get; set; }
            public float VyInit { get; set; }
            public string Shape { get; set; }
            public bool OpacityStatus { get; set; }
            public float Vo { get; set; }
            public bool SizeStatus { get; set; }
            public float Vs { get; set; }
        }

        private readonly ParticleConfig _config = new ParticleConfig();
        private readonly List<Particle> _particles = new List<Particle>();
        private Point _mousePos = Point.Empty;
        private bool _mouseMove = false;
        private Point _mouseClickPos = Point.Empty;
        private DateTime _mouseClickTime;
        private bool _bubbleClicking = false;
        private bool _repulseClicking = false;
        private float _pixelRatio = 1f;
        private Timer _renderTimer;
        #endregion

        public FrmParticles()
        {
            InitializeComponent();
            InitParticles();
            SetupRenderTimer();
        }

        #region 初始化
        private void InitializeComponent()
        {
            this.DoubleBuffered = true;
            this.BackColor = Color.Black;
            this.Size = new Size(800, 600);
            this.Text = "Particles.js C# Port";
            this.MouseMove += FrmParticles_MouseMove;
            this.MouseLeave += FrmParticles_MouseLeave;
            this.MouseClick += FrmParticles_MouseClick;
            this.Resize += FrmParticles_Resize;
        }

        private void InitParticles()
        {
            _particles.Clear();
            RetinaInit();
            CreateParticles();
            DensityAutoParticles();
        }

        private void RetinaInit()
        {
            _pixelRatio = _config.RetinaDetect && DisplayHelper.IsRetina() ? 2f : 1f;

            _config.Size *= _pixelRatio;
            _config.SizeAnimSpeed *= _pixelRatio;
            _config.MoveSpeed *= _pixelRatio;
            _config.LineDistance *= _pixelRatio;
            _config.GrabDistance *= _pixelRatio;
            _config.BubbleDistance *= _pixelRatio;
            _config.BubbleSize *= _pixelRatio;
            _config.RepulseDistance *= _pixelRatio;
            _config.LineWidth *= _pixelRatio;
        }

        private void CreateParticles()
        {
            Random rand = new Random();
            for (int i = 0; i < _config.ParticlesCount; i++)
            {
                Particle p = new Particle();
                InitSingleParticle(p, rand);
                _particles.Add(p);
            }
        }

        private void InitSingleParticle(Particle p, Random rand)
        {
            p.Radius = _config.SizeRandom ? (float)(rand.NextDouble() * _config.Size) : _config.Size;
            if (_config.SizeAnim)
            {
                p.SizeStatus = false;
                p.Vs = _config.SizeAnimSpeed / 100f;
                if (!_config.SizeSync) p.Vs *= (float)rand.NextDouble();
            }

            p.X = (float)(rand.NextDouble() * ClientSize.Width * _pixelRatio);
            p.Y = (float)(rand.NextDouble() * ClientSize.Height * _pixelRatio);

            if (p.X > ClientSize.Width * _pixelRatio - 2 * p.Radius) p.X -= p.Radius;
            else if (p.X < 2 * p.Radius) p.X += p.Radius;

            if (p.Y > ClientSize.Height * _pixelRatio - 2 * p.Radius) p.Y -= p.Radius;
            else if (p.Y < 2 * p.Radius) p.Y += p.Radius;

            p.Color = _config.ParticleColor;

            p.Opacity = _config.OpacityRandom ? (float)rand.NextDouble() * _config.Opacity : _config.Opacity;
            if (_config.OpacityAnim)
            {
                p.OpacityStatus = false;
                p.Vo = _config.OpacityAnimSpeed / 100f;
                if (!_config.OpacitySync) p.Vo *= (float)rand.NextDouble();
            }

            SetParticleDirection(p, rand);
            p.Shape = _config.ShapeType;
        }

        private void SetParticleDirection(Particle p, Random rand)
        {
            float vx = 0, vy = 0;
            switch (_config.MoveDirection)
            {
                case "top": vx = 0; vy = -1; break;
                case "top-right": vx = 0.5f; vy = -0.5f; break;
                case "right": vx = 1; vy = 0; break;
                case "bottom-right": vx = 0.5f; vy = 0.5f; break;
                case "bottom": vx = 0; vy = 1; break;
                case "bottom-left": vx = -0.5f; vy = 0.5f; break;
                case "left": vx = -1; vy = 0; break;
                case "top-left": vx = -0.5f; vy = -0.5f; break;
                default: vx = 0; vy = 0; break;
            }

            if (_config.MoveStraight)
            {
                p.Vx = vx;
                p.Vy = vy;
                if (_config.MoveRandom)
                {
                    p.Vx *= (float)rand.NextDouble();
                    p.Vy *= (float)rand.NextDouble();
                }
            }
            else
            {
                p.Vx = vx + (float)rand.NextDouble() - 0.5f;
                p.Vy = vy + (float)rand.NextDouble() - 0.5f;
            }

            p.VxInit = p.Vx;
            p.VyInit = p.Vy;
        }

        private void SetupRenderTimer()
        {
            _renderTimer = new Timer { Interval = 50 };
            _renderTimer.Tick += (s, e) => Invalidate();
            _renderTimer.Start();
        }
        #endregion

        #region 粒子更新与绘制
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            UpdateParticles();

            using (Bitmap buffer = new Bitmap(ClientSize.Width, ClientSize.Height))
            using (Graphics gBuffer = Graphics.FromImage(buffer))
            {
                gBuffer.ScaleTransform(1f / _pixelRatio, 1f / _pixelRatio);
                gBuffer.SmoothingMode = SmoothingMode.AntiAlias;

                DrawParticles(gBuffer);

                e.Graphics.DrawImage(buffer, 0, 0);
            }
        }

        private void UpdateParticles()
        {
            if (!_config.MoveEnable) return;

            foreach (var p in _particles)
            {
                float speed = _config.MoveSpeed / 2f;
                p.X += p.Vx * speed;
                p.Y += p.Vy * speed;

                UpdateParticleOpacity(p);
                UpdateParticleSize(p);

                HandleParticleBounds(p);

                if (_config.HoverEnable)
                    ProcessMouseInteractivity(p);
            }

            DrawParticleLinks();
        }

        private void UpdateParticleOpacity(Particle p)
        {
            if (!_config.OpacityAnim) return;

            if (p.OpacityStatus)
            {
                if (p.Opacity >= _config.Opacity) p.OpacityStatus = false;
                p.Opacity += p.Vo;
            }
            else
            {
                if (p.Opacity <= _config.OpacityMin) p.OpacityStatus = true;
                p.Opacity -= p.Vo;
            }

            if (p.Opacity < 0) p.Opacity = 0;
        }

        private void UpdateParticleSize(Particle p)
        {
            if (!_config.SizeAnim) return;

            if (p.SizeStatus)
            {
                if (p.Radius >= _config.Size) p.SizeStatus = false;
                p.Radius += p.Vs;
            }
            else
            {
                if (p.Radius <= _config.SizeMin) p.SizeStatus = true;
                p.Radius -= p.Vs;
            }

            if (p.Radius < 0) p.Radius = 0;
        }

        private void HandleParticleBounds(Particle p)
        {
            float w = ClientSize.Width * _pixelRatio;
            float h = ClientSize.Height * _pixelRatio;

            if (_config.OutMode == "bounce")
            {
                if (p.X + p.Radius > w) p.Vx = -p.Vx;
                if (p.X - p.Radius < 0) p.Vx = -p.Vx;
                if (p.Y + p.Radius > h) p.Vy = -p.Vy;
                if (p.Y - p.Radius < 0) p.Vy = -p.Vy;
            }
            else
            {
                if (p.X - p.Radius > w) { p.X = -p.Radius; p.Y = (float)new Random().NextDouble() * h; }
                if (p.X + p.Radius < 0) { p.X = w + p.Radius; p.Y = (float)new Random().NextDouble() * h; }
                if (p.Y - p.Radius > h) { p.Y = -p.Radius; p.X = (float)new Random().NextDouble() * w; }
                if (p.Y + p.Radius < 0) { p.Y = h + p.Radius; p.X = (float)new Random().NextDouble() * w; }
            }
        }

        private void DrawParticles(Graphics g)
        {
            foreach (var p in _particles)
            {
                float radius = p.RadiusBubble > 0 ? p.RadiusBubble : p.Radius;
                float opacity = p.OpacityBubble > 0 ? p.OpacityBubble : p.Opacity;
                Brush brush = new SolidBrush(Color.FromArgb((int)(opacity * 255), p.Color));

                switch (p.Shape)
                {
                    case "circle":
                        g.FillEllipse(brush, p.X - radius, p.Y - radius, radius * 2, radius * 2);
                        break;
                    case "edge":
                        g.FillRectangle(brush, p.X - radius, p.Y - radius, radius * 2, radius * 2);
                        break;
                    case "triangle":
                        DrawTriangle(g, brush, p.X, p.Y, radius);
                        break;
                    case "polygon":
                        DrawPolygon(g, brush, p.X, p.Y, radius, _config.ShapeSides);
                        break;
                    case "star":
                        DrawStar(g, brush, p.X, p.Y, radius, _config.ShapeSides);
                        break;
                }

                brush.Dispose();
            }
        }

        private void DrawParticleLinks()
        {
            if (!_config.LineLinked) return;

            using (Graphics g = CreateGraphics())
            {
                g.ScaleTransform(1f / _pixelRatio, 1f / _pixelRatio);
                g.SmoothingMode = SmoothingMode.AntiAlias;

                for (int i = 0; i < _particles.Count; i++)
                {
                    for (int j = i + 1; j < _particles.Count; j++)
                    {
                        Particle p1 = _particles[i];
                        Particle p2 = _particles[j];

                        float dx = p1.X - p2.X;
                        float dy = p1.Y - p2.Y;
                        float distance = (float)Math.Sqrt(dx * dx + dy * dy);

                        if (distance <= _config.LineDistance)
                        {
                            float opacity = _config.LineOpacity - (distance / _config.LineDistance) * _config.LineOpacity;
                            if (opacity > 0)
                            {
                                Pen pen = new Pen(Color.FromArgb((int)(opacity * 255), _config.LineColor), _config.LineWidth);
                                g.DrawLine(pen, p1.X, p1.Y, p2.X, p2.Y);
                                pen.Dispose();
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region 鼠标交互
        private void ProcessMouseInteractivity(Particle p)
        {
            if (!_mouseMove) return;

            if (_config.HoverMode == "grab") GrabParticle(p);
            if (_config.HoverMode == "bubble") BubbleParticle(p);
            if (_config.HoverMode == "repulse") RepulseParticle(p);
        }

        private void GrabParticle(Particle p)
        {
            float dx = p.X - _mousePos.X * _pixelRatio;
            float dy = p.Y - _mousePos.Y * _pixelRatio;
            float distance = (float)Math.Sqrt(dx * dx + dy * dy);

            if (distance <= _config.GrabDistance)
            {
                float opacity = _config.GrabLineOpacity - (distance / _config.GrabDistance) * _config.GrabLineOpacity;
                if (opacity > 0)
                {
                    using (Graphics g = CreateGraphics())
                    {
                        g.ScaleTransform(1f / _pixelRatio, 1f / _pixelRatio);
                        Pen pen = new Pen(Color.FromArgb((int)(opacity * 255), _config.LineColor), _config.LineWidth);
                        g.DrawLine(pen, p.X, p.Y, _mousePos.X * _pixelRatio, _mousePos.Y * _pixelRatio);
                        pen.Dispose();
                    }
                }
            }
        }

        private void BubbleParticle(Particle p)
        {
            float dx = p.X - _mousePos.X * _pixelRatio;
            float dy = p.Y - _mousePos.Y * _pixelRatio;
            float distance = (float)Math.Sqrt(dx * dx + dy * dy);

            if (distance <= _config.BubbleDistance)
            {
                float factor = 1 - (distance / _config.BubbleDistance);
                p.RadiusBubble = _config.BubbleSize * factor;
                p.OpacityBubble = _config.BubbleOpacity * factor;
            }
            else
            {
                p.RadiusBubble = 0;
                p.OpacityBubble = 0;
            }
        }

        private void RepulseParticle(Particle p)
        {
            if (!_repulseClicking) return;

            float dx = p.X - _mouseClickPos.X * _pixelRatio;
            float dy = p.Y - _mouseClickPos.Y * _pixelRatio;
            float distSq = dx * dx + dy * dy;
            if (distSq < 1) distSq = 1;

            float repulseForce = (float)Math.Pow(_config.RepulseDistance / 6, 3) / distSq * 5;
            p.Vx += dx / (float)Math.Sqrt(distSq) * repulseForce;
            p.Vy += dy / (float)Math.Sqrt(distSq) * repulseForce;
        }

        private void PushParticles(int count)
        {
            Random rand = new Random();
            for (int i = 0; i < count; i++)
            {
                Particle p = new Particle();
                p.X = _mouseClickPos.X * _pixelRatio;
                p.Y = _mouseClickPos.Y * _pixelRatio;
                InitSingleParticle(p, rand);
                _particles.Add(p);
            }
        }

        private void RemoveParticles(int count)
        {
            if (_particles.Count >= count)
                _particles.RemoveRange(0, count);
        }
        #endregion

        #region 形状绘制
        private void DrawTriangle(Graphics g, Brush brush, float x, float y, float size)
        {
            PointF[] points = {
                new PointF(x, y - size),
                new PointF(x - size, y + size/2),
                new PointF(x + size, y + size/2)
            };
            g.FillPolygon(brush, points);
        }

        private void DrawPolygon(Graphics g, Brush brush, float x, float y, float radius, int sides)
        {
            PointF[] points = new PointF[sides];
            double step = 2 * Math.PI / sides;
            for (int i = 0; i < sides; i++)
            {
                double angle = i * step - Math.PI / 2;
                points[i] = new PointF(
                    x + (float)(radius * Math.Cos(angle)),
                    y + (float)(radius * Math.Sin(angle))
                );
            }
            g.FillPolygon(brush, points);
        }

        private void DrawStar(Graphics g, Brush brush, float x, float y, float radius, int points)
        {
            List<PointF> starPoints = new List<PointF>();
            double step = Math.PI / points;
            for (int i = 0; i < 2 * points; i++)
            {
                double angle = i * step - Math.PI / 2;
                float r = i % 2 == 0 ? radius : radius / 2;
                starPoints.Add(new PointF(
                    x + (float)(r * Math.Cos(angle)),
                    y + (float)(r * Math.Sin(angle))
                ));
            }
            g.FillPolygon(brush, starPoints.ToArray());
        }
        #endregion

        #region 事件
        private void FrmParticles_MouseMove(object sender, MouseEventArgs e)
        {
            _mousePos = e.Location;
            _mouseMove = true;
        }

        private void FrmParticles_MouseLeave(object sender, EventArgs e)
        {
            _mouseMove = false;
            foreach (var p in _particles)
            {
                p.RadiusBubble = 0;
                p.OpacityBubble = 0;
            }
        }

        private void FrmParticles_MouseClick(object sender, MouseEventArgs e)
        {
            if (!_config.ClickEnable) return;

            _mouseClickPos = e.Location;
            _mouseClickTime = DateTime.Now;

            switch (_config.ClickMode)
            {
                case "push":
                    PushParticles(_config.PushCount);
                    break;
                case "remove":
                    RemoveParticles(_config.RemoveCount);
                    break;
                case "repulse":
                    _repulseClicking = true;
                    Task.Delay((int)(_config.RepulseDuration * 1000)).ContinueWith(t => _repulseClicking = false);
                    break;
                case "bubble":
                    _bubbleClicking = true;
                    Task.Delay((int)(_config.BubbleDuration * 1000)).ContinueWith(t => _bubbleClicking = false);
                    break;
            }
        }

        private void FrmParticles_Resize(object sender, EventArgs e)
        {
            InitParticles();
        }

        private void DensityAutoParticles()
        {
            if (!_config.DensityEnable) return;

            float area = ClientSize.Width * ClientSize.Height / 1000f;
            if (_pixelRatio > 1) area /= 2 * _pixelRatio;

            int targetCount = (int)(area * _config.ParticlesCount / _config.DensityArea);
            int diff = _particles.Count - targetCount;

            if (diff < 0) PushParticles(-diff);
            else if (diff > 0) RemoveParticles(diff);
        }
        #endregion

        private static class DisplayHelper
        {
            public static bool IsRetina()
            {
                return Screen.PrimaryScreen.BitsPerPixel > 32;
            }
        }
    }
}
