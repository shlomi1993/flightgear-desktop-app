#pragma once
#define DEC __declspec( dllexport )

// Returns the average of the floats in array x.
extern "C" DEC float avg(float* x, int size);

// Returns the variance of x and x.
extern "C" DEC float var(float* x, int size);

// Returns the covariance of x and y.
extern "C" DEC float cov(float* x, float* y, int size);

// Returns the Pearson correlation coefficient of x and y.
extern "C" DEC float pearson(float* x, float* y, int size);

// Defines line.
class Line {
public:
	float a, b;
	Line() :a(0), b(0) {};
	Line(float a, float b) :a(a), b(b) {}
	float f(float x) {
		return a * x + b;
	}
};

// Line exporter.
extern "C" DEC void* Line_new() { return (void *) new Line(); }
extern "C" DEC void* Line_new2(float a, float b) { return (void *) new Line(a, b); }
extern "C" DEC float Line_f(Line *l, float x) { return l->f(x); }

// Defines point.
class Point {
public:
	float x, y;
	Point(float x, float y) :x(x), y(y) {}
	Point() :x(0), y(0) {}
};

// Point exporter.
extern "C" DEC void* Point_new() { return (void*) new Point(); }
extern "C" DEC void* Point_new2(float a, float b) { return (void*) new Point(a, b); }

// Performs a linear regression and returns the line equation.
extern "C" DEC Line linear_reg(Point** points, int size);

// Returns the deviation between point p and the line equation of the points.
extern "C" DEC float devp(Point p, Point** points, int size);

// Returns the deviation between point p and the line.
extern "C" DEC float devl(Point p, Line l);
