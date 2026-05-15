import http from 'k6/http';
import { check, sleep } from 'k6';

const BASE_URL = (__ENV.BASE_URL || 'http://a-puchkina-explanatorynotesrv.laop.ulstu.ru/polina')
  .replace(/\/$/, '');

const LOGIN = __ENV.LOGIN || 'p.chubykina';

export const options = {
  thresholds: {
    http_req_failed: ['rate<0.05'],
    http_req_duration: ['p(95)<2000'],
    checks: ['rate>0.95'],
  },
};

export default function () {
  let res;

  res = http.get(`${BASE_URL}/api/core/AuthProfile/GetProfile?login=${encodeURIComponent(LOGIN)}`);
  check(res, {
    'AuthProfile 200': (r) => r.status === 200,
  });

  res = http.get(`${BASE_URL}/api/core/Lecturers/GetLecturerList`);
  check(res, {
    'Lecturers 200': (r) => r.status === 200,
  });

  res = http.get(`${BASE_URL}/api/core/Students/GetStudentList`);
  check(res, {
    'Students 200': (r) => r.status === 200,
  });

  sleep(1);
}