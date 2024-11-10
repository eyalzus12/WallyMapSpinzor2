using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace WallyMapSpinzor2;

public static class BrawlhallaMath
{
    //i have no fucking idea how this works
    //brawlhalla uses this to compute the weight for lerping
    public static double EaseWeight(double weight, bool easeIn, bool easeOut, uint easePower)
    {
        if (!easeIn && !easeOut) return weight;

        if (easeIn && easeOut) weight *= 2;
        int num1 = easePower == 2 ? 0 : 1;
        double num2 = 1;
        if (easeIn && easeOut)
        {
            num1++;
            easeIn = weight < 1;
            num2 = 0.5;
        }

        if (easeIn)
        {
            weight = Math.Pow(weight, easePower);
        }
        else if (easeOut)
        {
            if (easePower == 2)
            {
                weight = (weight - num1) * (weight - num1 - 2) - num1;
            }
            else if (easePower > 2)
            {
                weight = ((easePower & 1) << num1) + Math.Pow(weight - num1, easePower) - num1;
            }

            if ((easePower & 1) == 0)
            {
                num2 = -num2;
            }
        }

        return num2 * weight;
    }

    // create a list of points used for collision anchors
    // does not include the starting point
    // when rendering pairs of points as a line, make sure to flip them so the first has a lower X
    // to get the correct collision normal
    public static IEnumerable<(double, double)> CollisionQuad(double x1, double y1, double x2, double y2, double xa, double ya)
    {
        int segments = (int)Math.Round((Math.Abs(x2 - xa) + Math.Abs(x1 - xa) + Math.Abs(y2 - ya) + Math.Abs(y1 - ya)) / 125);
        if (segments < 4) segments = 4;
        if (segments > 10) segments = 10;
        for (int i = 1; i <= segments; ++i)
        {
            double fraction = i / (double)segments;
            double offsetX0 = (xa - x1) * fraction;
            double offsetX1 = (xa - x2) * (1 - fraction);
            double newX = (x1 + offsetX0) * (1 - fraction) + (x2 + offsetX1) * fraction;
            double offsetY0 = (ya - y1) * fraction;
            double offsetY1 = (ya - y2) * (1 - fraction);
            double newY = (y1 + offsetY0) * (1 - fraction) + (y2 + offsetY1) * fraction;

            yield return (newX, newY);
        }
    }

    public static double BrawlhallaCos(double x)
    {
        // im not using Math.TAU here because brawlhalla uses Math.PI * 2
        // which may introduce error. this will probably never matter but eh.
        x -= Math.Floor(x / (Math.PI * 2)) * (Math.PI * 2);
        x = Math.Round(x * 1000) / 1000;
        x = Math.Cos(x);
        x = Math.Round(x * 1000) / 1000;
        return x;
    }

    public static double BrawlhallaSin(double x)
    {
        // im not using Math.TAU here because brawlhalla uses Math.PI * 2
        // which may introduce error. this will probably never matter but eh.
        x -= Math.Floor(x / (Math.PI * 2)) * (Math.PI * 2);
        x = Math.Round(x * 1000) / 1000;
        x = Math.Sin(x);
        x = Math.Round(x * 1000) / 1000;
        return x;
    }

    public static double Length(double x, double y) => Math.Sqrt(x * x + y * y);
    public static (double, double) Normalize(double x, double y)
    {
        double len = Length(x, y);
        if (len == 0) return (0, 0);
        return (x / len, y / len);
    }

    public static (double, double) BrawlhallaLerp(double x1, double y1, double x2, double y2, double w)
    {
        // uses the same rounding as brawlhalla
        double x = Math.Round((x1 * (1 - w) + x2 * w) * 100) / 100;
        double y = Math.Round((y1 * (1 - w) + y2 * w) * 100) / 100;
        return (x, y);
    }

    public static double Dot(double X1, double Y1, double X2, double Y2) => X1 * Y1 + X2 * Y2;
    public static double Cross(double X1, double Y1, double X2, double Y2) => X1 * Y2 - X2 * Y1;

    public static double AngleBetween(double X1, double Y1, double X2, double Y2) =>
        Math.Atan2(Cross(X1, Y1, X2, Y2), Dot(X1, Y1, X2, Y2));

    public static (double, double) Rotated(double X, double Y, double t)
    {
        (double sine, double cosi) = Math.SinCos(t);
        return (X * cosi - Y * sine, X * sine + Y * cosi);
    }

    public static (double, double) Slerp(double X1, double Y1, double X2, double Y2, double w)
    {
        (double _X1, double _Y1) = Normalize(X1, Y1);
        (double _X2, double _Y2) = Normalize(X2, Y2);
        double t = AngleBetween(_X1, _Y1, _X2, _Y2);
        (double _X, double _Y) = Rotated(_X1, _Y1, w * t);
        return (_X * Math.Abs(X1 - X2), _Y * Math.Abs(Y1 - Y2));
    }

    public static (double, double) BrawlhallaLerpWithCenter(double x1, double y1, double x2, double y2, double xc, double yc, double w)
    {
        // code follows brawlhalla logic. brawlhalla does not do a proper Slerp
        // but instead assumes center will always be on the same X or Y as one of the keyframes

        double angle1;
        if (x1 == xc)
        {
            if (y1 > yc) angle1 = Math.PI * 0.5;
            else angle1 = Math.PI * 1.5;
        }
        else if (x1 < xc) angle1 = Math.PI;
        else angle1 = 0;

        double angle2;
        if (x2 == xc)
        {
            if (y2 > yc) angle2 = Math.PI * 0.5;
            else angle2 = Math.PI * 1.5;
        }
        else if (x2 < xc) angle2 = Math.PI;
        else if (angle1 == Math.PI * 1.5) angle2 = Math.PI * 2;
        else angle2 = 0;

        if (angle1 == 0 && angle2 == Math.PI * 1.5)
            angle1 = Math.PI * 2;

        double angle = angle1 * (1 - w) + angle2 * w;
        double x = Math.Round((xc + Math.Abs(x1 - x2) * BrawlhallaCos(angle)) * 100) / 100;
        double y = Math.Round((yc + Math.Abs(y1 - y2) * BrawlhallaSin(angle)) * 100) / 100;
        return (x, y);
    }

    //this might be an overkill
    public static T1 SafeMod<T1, T2>(T1 x, T2 m)
        where T1 :
            INumber<T1>,
            IModulusOperators<T1, T2, T1>,
            IAdditionOperators<T1, T2, T1>
    {
        x %= m;
        if (x < T1.Zero) x += m;
        return x;
    }

    // ported directly from brawlhalla.
    // TODO: figure out what this does.
    private static double RaycastHelper(double param1, double param2, double param3, double param4, double param5, double param6, ref Position param7)
    {
        double _loc8_ = -param6;
        double _loc9_ = param5;
        double _loc10_ = Math.Sqrt(_loc8_ * _loc8_ + _loc9_ * _loc9_);
        double _loc11_;
        if (_loc10_ != 0)
        {
            _loc11_ = -1 / _loc10_;
            _loc8_ *= _loc11_;
            _loc9_ *= _loc11_;
        }
        param7 = new(_loc8_, _loc9_);

        _loc11_ = param1 - param3;
        double _loc12_ = param2 - param4;
        return _loc11_ * _loc8_ + _loc12_ * _loc9_;
    }

    // ported directly from brawlhalla.
    // TODO: figure out what this does.
    private static bool RaycastHelper2(double param1, double param2, double param3, double param4, double param5, double param6, double param7, double param8, ref Position? param9)
    {
        double _loc10_ = (param8 - param6) * (param3 - param1) - (param7 - param5) * (param4 - param2);
        if (_loc10_ == 0)
        {
            return false;
        }
        double _loc11_ = 1 / _loc10_;
        double _loc12_ = _loc11_ * ((param7 - param5) * (param2 - param6) - (param8 - param6) * (param1 - param5));
        double _loc13_ = _loc11_ * ((param3 - param1) * (param2 - param6) - (param4 - param2) * (param1 - param5));
        if (_loc12_ >= 0 && _loc12_ <= 1 && _loc13_ >= 0 && _loc13_ <= 1)
        {
            if (param9 is not null)
                param9 = new(param1 + _loc12_ * (param3 - param1), param2 + _loc12_ * (param4 - param2));
            return true;
        }
        return false;
    }

    // a hopefully-accurate port of brawlhalla's raycast function
    // the game does some kind of quad tree impl to filter out collisions
    // not implemented here
    // also 3 minor checks are commented out because they're unused or annoying to implement
    public static AbstractCollision? Raycast(
        IEnumerable<AbstractCollision> collisions,
        int team /* param1 */,
        double fromX /*param2*/, double fromY /*param3*/,
        ref Position len /*param4*/, ref Position pos /*param5*/,
        AbstractCollision? exclude /*param6*/,
        ref Position? param7, ref Position? param8,
        CollisionTypeFlags mandateFlags /*param9*/,
        // 1 - allow detect soft from below. 2 - exclude team collisions. 4 - ?. 8 - ignore collisions from the wrong side.
        uint raycastFlags, /* param10 */
        /*int param11 = 0,*/
        CollisionTypeFlags excludeFlags = 0 /*param12*/,
        HashSet<AbstractCollision>? outList = null /*param13*/
    )
    {
        CollisionTypeFlags soft = CollisionTypeFlags.SOFT;

        double lenX = len.X; //_loc14_
        double lenY = len.Y; //_loc15_
        if (lenX == 0 && lenY == 0) return null;
        Position tempPos = new(0, 0);
        double posX = fromX + len.X; //_loc19_
        double posY = fromY + len.Y; //_loc20_
        AbstractCollision? result = null; //_loc18_
        foreach (AbstractCollision col /*_loc16_*/ in collisions)
        {
            if ((col.CollisionType & mandateFlags) == 0) continue;

            // this is seemingly unused
            //if(col.§_-s13§) continue;

            // this is related to a deprecated "camera zone" thing from the swf map days
            //if(param11 != -1 && col.§_-5u§ != param11) continue;

            if (col.Team != 0 && col.Team == team) continue;
            if ((col.CollisionType & excludeFlags) != 0) continue;
            if ((raycastFlags & 2) != 0 && col.Team != 0) continue;
            if (col == exclude) continue;

            // this seems to give priority to hard collisions over soft collisions when both are overlaping floors and the hard collision is a moving collision that is currently not moving
            //if (result is not null && result.NormalY == -1 && (result.CollisionType & hard) != 0 && (col.CollisionType & soft) != 0 && col.NormalY == -1 && result.FromY == col.FromY && outList is null && col.§_-l1I§ == col.startX) continue;

            if ((raycastFlags & (1 | 4 | 8)) == 0 && (col.NormalX != 0 || col.NormalY != 0))
            {
                if (col.FromX == col.ToX)
                {
                    if (len.X != 0 && (len.X > 0) == (col.NormalX > 0)) continue;
                }
                else if (col.FromY == col.ToY)
                {
                    if (len.Y != 0 && (len.Y > 0) == (col.NormalY > 0)) continue;
                }
                else if (len.X == 0)
                {
                    if ((len.Y > 0) == (col.NormalY > 0)) continue;
                }
                else if (len.Y == 0)
                {
                    if ((len.X > 0) == (col.NormalX > 0)) continue;
                }
                else if ((len.X > 0) == (col.NormalX > 0) && (len.Y > 0) == (col.NormalY > 0))
                {
                    continue;
                }
            }

            double _loc17_ = RaycastHelper(fromX, fromY, col.FromX, col.FromY, col.ToX - col.FromX, col.ToY - col.FromY, ref tempPos);
            if (_loc17_ >= 0 || (mandateFlags & soft) == 0 || (col.CollisionType & soft) == 0 || (raycastFlags & 1) != 0)
            {
                if (RaycastHelper2(fromX, fromY, posX, posY, col.FromX, col.FromY, col.ToX, col.ToY, ref param7))
                {
                    result = col;
                    if (param8 is not null) param8 = param8.Value with { X = _loc17_ };

                    if (outList is null)
                    {
                        posX = tempPos.X;
                        posY = tempPos.Y;
                        lenX = posX - fromX;
                        lenY = posY - fromY;
                    }
                    else
                        outList.Add(col);
                }
            }
        }

        if ((raycastFlags & 4) != 0 && result is not null)
        {
            if (result.NormalY < 0 && len.Y < 0 && lenY > len.Y)
            {
                result = null;
            }
            else if (result.NormalY > 0 && len.Y > 0 && lenY < len.Y)
            {
                result = null;
            }
            else if (result.NormalX < 0 && len.X < 0 && lenX > len.X)
            {
                result = null;
            }
            else if (result.NormalX > 0 && len.X > 0 && lenX < len.X)
            {
                result = null;
            }
        }

        if (result is not null)
        {
            pos = tempPos;
            len = new(lenX, lenY);
            return result;
        }

        return null;
    }

    // horde random path generation
    public static IEnumerable<(double, double)> GenerateHordePath(
        BrawlhallaRandom rand, // random number generator
        double boundX, double boundY, double boundW, double boundH, // camera bounds
        double door1CX, double door1CY, // door 1
        double door2CX, double door2CY, // door 2
        DirEnum dir, PathEnum path, // type
        int idx // path index
    )
    {
        return _().SelectMany(_ => _);

        // hack: yield return multiple IEnumerable's, and then SelectMany them to flatten the list
        IEnumerable<IEnumerable<(double, double)>> _()
        {
            idx %= 20;
            switch (dir)
            {
                case DirEnum.TOP:
                    {
                        double jump = boundW / 10;
                        (double fromX, double fromY) = (boundX + idx * jump, boundY);
                        (double doorX, double doorY) = PickDoor(path, fromX < boundW / 2) ? (door1CX, door1CY) : (door2CX, door2CY);
                        if (rand.Next() % 4 == 0)
                        {
                            bool idfk = Math.Abs(doorX - fromX) >= boundW / 3;
                            (double midX, double midY) = (doorX + (((fromX > doorX) == idfk) ? 1 : -1) * jump, 1000);
                            yield return GeneratePathSegment(fromX, fromY, midX, midY, 2, true, true);
                            yield return GeneratePathSegment(midX, midY, doorX, doorY, 2, false, false);
                        }
                        else
                        {
                            yield return GeneratePathSegment(fromX, fromY, doorX, doorY, 2, true, false);
                        }
                    }
                    break;
                case DirEnum.RIGHT:
                    {
                        double jump = boundH / 10;
                        (double fromX, double fromY) = (boundX + boundW, boundY + idx * jump);
                        (double doorX, double doorY) = PickDoor(path, false) ? (door1CX, door1CY) : (door2CX, door2CY);
                        if (path == PathEnum.FAR && rand.Next() % 3 == 0)
                        {
                            (double midX1, double midY1) = (3220, 1050);
                            (double midX2, double midY2) = (2220, 850);
                            yield return GeneratePathSegment(fromX, fromY, midX1, midY1, 2, true, true);
                            yield return GeneratePathSegment(midX1, midY1, midX2, midY2, 2, false, true);
                            yield return GeneratePathSegment(midX2, midY2, doorX, doorY, 2, false, false);
                        }
                        else
                        {
                            (double midX, double midY) = (3220, 1300);
                            yield return GeneratePathSegment(fromX, fromY, midX, midY, 3, true, true);
                            yield return GeneratePathSegment(midX, midY, doorX, doorY, 3, false, false);
                        }
                    }
                    break;
                case DirEnum.BOTTOM:
                    {
                        double jump = boundW / 20;
                        (double fromX, double fromY) = (boundX + idx * jump, boundY + boundH + 100);
                        double doorX, doorY;
                        double midX1, midY1 = 2800;
                        double midX2, midY2 = 1600;
                        if (fromX < boundX + boundW / 3)
                        {
                            (doorX, doorY) = PickDoor(PathEnum.CLOSE, true) ? (door1CX, door1CY) : (door2CX, door2CY);
                            midX1 = -650;
                            midX2 = -550;
                        }
                        else if (fromX > boundX + 2 * boundW / 3)
                        {
                            (doorX, doorY) = PickDoor(PathEnum.CLOSE, false) ? (door1CX, door1CY) : (door2CX, door2CY);
                            midX1 = 3320;
                            midX2 = 3220;
                        }
                        else
                        {
                            (doorX, doorY) = PickDoor(PathEnum.CLOSE, fromX < boundX + boundW / 2) ? (door1CX, door1CY) : (door2CX, door2CY);
                            bool chance50 = rand.Next() % 2 == 0;
                            bool chance25 = rand.Next() % 4 == 0;
                            if (chance50)
                            {
                                midX1 = 1201;
                                midX2 = chance25 ? 1461 : 1201;
                            }
                            else
                            {
                                midX1 = 1461;
                                midX2 = chance25 ? 1201 : 1461;
                            }
                        }

                        yield return GeneratePathSegment(fromX, fromY, midX1, midY1, 3, true, true);
                        yield return GeneratePathSegment(midX1, midY1, midX2, midY2, 3, false, true);
                        yield return GeneratePathSegment(midX2, midY2, doorX, doorY, 3, false, false);
                    }
                    break;
                case DirEnum.LEFT:
                    {
                        double jump = boundH / 10;
                        (double fromX, double fromY) = (boundX, boundY + idx * jump);
                        (double doorX, double doorY) = PickDoor(path, true) ? (door1CX, door1CY) : (door2CX, door2CY);
                        if (path == PathEnum.FAR && rand.Next() % 3 == 0)
                        {
                            (double midX1, double midY1) = (-550, 1050);
                            (double midX2, double midY2) = (1450, 850);

                            yield return GeneratePathSegment(fromX, fromY, midX1, midY1, 2, true, true);
                            yield return GeneratePathSegment(midX1, midY1, midX2, midY2, 2, false, true);
                            yield return GeneratePathSegment(midX2, midY2, doorX, doorY, 2, false, false);
                        }
                        else
                        {
                            (double midX, double midY) = (-550, 1300);
                            yield return GeneratePathSegment(fromX, fromY, midX, midY, 3, true, true);
                            yield return GeneratePathSegment(midX, midY, doorX, doorY, 3, false, false);
                        }
                    }
                    break;
            }
        }

        bool PickDoor(PathEnum path, bool left) =>
            (path == PathEnum.ANY) ? (rand.Next() % 2 == 0) : (left == (path == PathEnum.CLOSE));

        IEnumerable<(double, double)> GeneratePathSegment(double x1, double y1, double x2, double y2, int parts, bool first, bool middle)
        {
            if (first)
                yield return (x1, y1);

            for (int i = 0; i < parts; ++i)
            {
                if (i == parts - 1 && !middle)
                    yield return (x2, y2);
                else
                {
                    double xseg1 = (x2 - x1) / (parts - i);
                    double yseg1 = (y2 - y1) / (parts - i);
                    double xseg2 = xseg1 + x1;
                    double yseg2 = yseg1 + y1;
                    double xSign = xseg1 >= 0 ? 1 : -1;
                    double ySign = yseg1 >= 0 ? 1 : -1;
                    xseg1 = Math.Abs(xseg1);
                    yseg1 = Math.Abs(yseg1);
                    xseg1 = (xseg1 < 15) ? 15 : ((150 < xseg1) ? 150 : xseg1);
                    yseg1 = (yseg1 < 15) ? 15 : ((150 < yseg1) ? 150 : yseg1);
                    xseg1 *= xSign;
                    yseg1 *= ySign;
                    double xResult = xseg2 - xseg1 + (rand.Next() % (2 * xseg1));
                    double yResult = yseg2 - yseg1 + (rand.Next() % (2 * yseg1));
                    yield return (xResult, yResult);
                }
            }
        }
    }
}