import os
import psycopg2
import datetime

def getConnection():
    conn = psycopg2.connect(
        host="postgresql-152026-0.cloudclusters.net",
        database="ndgradgate",
        user="alex",
        password="12345678",
        port="10029")
    return conn

def getCursor(conn):
    return conn.cursor()

def with_connection(func):
    def wrapper(*args):
        conn = getConnection()
        cur = getCursor(conn)
        func(cur, *args)
        conn.commit()
        cur.close()
        conn.close()

    return wrapper

@with_connection
def getReviewers(cur):
    cur.execute("SELECT * FROM admin_gradgate.reviewers;")

    rows = cur.fetchall()
    for row in rows:
        print(row)

@with_connection
def assign_applicant_reviewer(cur, faculty_id, applicant_id):
    cur.execute(f"INSERT INTO gradgate.reviewer_assignment VALUES ({faculty_id}, {applicant_id}, NULL, 'In Progress');")

@with_connection
def make_reviewer(cur, faculty_id):
    cur.execute(f"UPDATE gradgate.faculty SET is_reviewer = TRUE WHERE faculty_id = {faculty_id};")

@with_connection
def remove_reviewer(cur, faculty_id):
    cur.execute(f"UPDATE gradgate.faculty SET is_reviewer = FALSE WHERE faculty_id = {faculty_id};")

@with_connection
def make_comment(cur, faculty_id, applicant_id, content):
    cur.execute(f"INSERT INTO gradgate.comments (faculty_id, applicant_id, content) VALUES ({faculty_id}, {applicant_id}, '{content}');")
    cur.execute(f"SELECT * FROM gradgate.comments WHERE faculty_id={faculty_id} and applicant_id={applicant_id};")
    id = cur.fetchall()[0][0]

    cur.execute(f"UPDATE gradgate.reviewer_assignment SET comment_id = {id}, status='Finished' WHERE faculty_id = {faculty_id} and applicant_id = {applicant_id};")

@with_connection
def make_decision(cur, applicant_id, decision):
    cur.execute(f"UPDATE gradgate.applicants SET decision = '{decision}' WHERE applicant_id = {applicant_id};")

@with_connection
def suggest_advisor(cur, faculty_id, applicant_id):
    cur.execute(f"INSERT INTO gradgate.potential_advisors VALUES ({faculty_id}, {applicant_id});")

suggest_advisor(8, 8)
